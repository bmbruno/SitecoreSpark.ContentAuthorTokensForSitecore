using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SitecoreSpark.CATS.Infrastructure.Extensions;
using Sitecore.Data;
using SitecoreSpark.CATS.Models;

namespace SitecoreSpark.CATS.Infrastructure
{
    public static class TokenManager
    {
        public const string TOKEN_DATABASE = "master";

        /// <summary>
        /// Loads all token library items in Sitecore; first the default library, then any user definied libraries that are set up on the CATS configuration item.
        /// </summary>
        /// <returns>List of token library Items.</returns>
        public static IEnumerable<Item> GetAllTokenLibraries()
        {
            Database tokenDB = Database.GetDatabase(TOKEN_DATABASE);
            List<Item> libraries = new List<Item>();

            // Default library
            Item defaultLibrary = tokenDB.GetItem(Constants.CATS_Default_Library_ID);

            if (defaultLibrary == null)
                throw new Exception($"No library item found with ID: {Constants.CATS_Default_Library_ID}");

            libraries.Add(defaultLibrary);

            // Check for additional libraries
            Item configItem = GetConfigurationItem();
            string[] libraryItems = configItem.GetTreelistValuesRaw("Libraries");
            
            foreach (string itemID in libraryItems)
            {
                Item libraryItem = tokenDB.GetItem(new ID(itemID));

                if (libraryItem == null)
                    continue;

                libraries.Add(libraryItem);
            }

            return libraries;

        }

        /// <summary>
        /// Gets a list of all tokens in a list of libraries. Tokens missing a Pattern or Value will not be loaded.
        /// </summary>
        /// <param name="libraries">Libraries to load tokens from.</param>
        /// <returns>List of Token objects.</returns>
        public static IEnumerable<Token> GetTokensFromLibraries(IEnumerable<Item> libraries)
        {
            List<Token> tokens = new List<Token>();

            foreach (Item library in libraries)
            {
                IEnumerable<Item> tokensInLibrary = library.Children.Where(u => u.TemplateID == new ID(Constants.CATS_Token_Template_ID)).ToList();

                foreach (Item token in tokensInLibrary)
                {
                    string pattern = token["Pattern"];
                    string value = token["Value"];

                    bool validToken = true;
                    if (String.IsNullOrEmpty(pattern))
                    {
                        Logger.Warn($"Missing pattern for token item {token.ID}; will not be cached or utilized.", typeof(TokenManager));
                        validToken = false;
                    }

                    if (String.IsNullOrEmpty(value))
                    {
                        Logger.Warn($"Missing value for token item {token.ID}; will not be cached or utilized.", typeof(TokenManager));
                        validToken = false;
                    }

                    if (validToken)
                    {
                        tokens.Add(new Token()
                        {
                            Pattern = pattern,
                            Value = value
                        });
                    }
                }
            }

            return tokens;
        }

        /// <summary>
        /// Gets the module root configuration item from Sitecore (/sitecore/system/modules/Content Author Tokens).
        /// </summary>
        /// <returns>Sitecore Item of the module root object.</returns>
        public static Item GetConfigurationItem()
        {
            Item configItem = Sitecore.Data.Database.GetDatabase(TOKEN_DATABASE).GetItem(Constants.CATS_Configuration_Item_ID);

            if (configItem == null)
                throw new Exception($"No configuration item found with ID: {Constants.CATS_Default_Library_ID}. Please re-install the CATS module.");

            return configItem;
        }
    }
}