﻿using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using SitecoreSpark.CATS.Infrastructure.Extensions;
using Sitecore.Data;
using SitecoreSpark.CATS.Models;

namespace SitecoreSpark.CATS.Infrastructure
{
    /// <summary>
    /// Provides access to content database token information. ALso some configuration item access.
    /// </summary>
    public static class TokenService
    {
        /// <summary>
        /// Loads all token library items in Sitecore; first the default library, then any user definied libraries that are set up on the CATS configuration item.
        /// </summary>
        /// <returns>List of token library Items.</returns>
        public static IEnumerable<Item> GetAllTokenLibraries()
        {
            string webDatabase = Sitecore.Configuration.Settings.GetSetting("SitecoreSpark.CATS.PublishedDatabase");

            Database tokenDB = Database.GetDatabase(webDatabase);
            List<Item> libraries = new List<Item>();
            Item configItem = GetConfigurationItem();
            
            // All Token Libraries under the CATS module node
            Item[] moduleLibraries = configItem.Axes.GetDescendants();
            moduleLibraries = moduleLibraries.Where(u => u.TemplateID == new ID(Constants.CATS_TokenLibrary_Template_ID)).ToArray();
                
            if (moduleLibraries == null)
                throw new Exception($"No library item found under default CATS configuration item ({Constants.CATS_Configuration_Item_ID}).");

            libraries.AddRange(moduleLibraries);

            // Check for additional libraries
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
        public static IEnumerable<ContentToken> GetTokensFromLibraries(IEnumerable<Item> libraries, bool onlyUserTokens = false)
        {
            List<ContentToken> tokens = new List<ContentToken>();

            foreach (Item library in libraries)
            {
                IEnumerable<Item> tokensInLibrary = library.Children.Where(u => u.TemplateID == new ID(Constants.CATS_Token_Template_ID)).ToList();

                foreach (Item token in tokensInLibrary)
                {
                    string pattern = token["Pattern"];
                    string value = token["Output"];

                    if (onlyUserTokens && pattern.StartsWith("_CATS_"))
                        continue;

                    if (String.IsNullOrEmpty(pattern))
                        Logger.Warn($"Missing Pattern for token item {token.ID}; will not be cached or rendered.", typeof(TokenService));

                    if (String.IsNullOrEmpty(value))
                        Logger.Warn($"Missing Output value for token item {token.ID}; will not be cached or rendered.", typeof(TokenService));

                    tokens.Add(new ContentToken()
                    {
                        ItemID = token.ID.Guid,
                        Pattern = pattern,
                        Output = value
                    });
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
            Item configItem = Sitecore.Data.Database.GetDatabase(GetCurrentDatabaseName()).GetItem(Constants.CATS_Configuration_Item_ID);

            if (configItem == null)
                throw new Exception($"No configuration item found with ID: {Constants.CATS_Configuration_Item_ID}. Please re-install the CATS module.");

            return configItem;
        }

        /// <summary>
        /// Gets the token start tag from configuration.
        /// </summary>
        /// <returns>String of token tag.</returns>
        public static string GetTokenStartTag()
        {
            string rawValue = Sitecore.Configuration.Settings.GetSetting("SitecoreSpark.CATS.StartTag");

            if (String.IsNullOrEmpty(rawValue))
                throw new Exception("SitecoreSpark.CATS.StartTag is empty! Check SitecoreSpark.CATS.Settings.config file.");

            return rawValue;
        }

        /// <summary>
        /// Gets the token end tag from configuration.
        /// </summary>
        /// <returns>String of token tag.</returns>
        public static string GetTokenEndTag()
        {
            string rawValue = Sitecore.Configuration.Settings.GetSetting("SitecoreSpark.CATS.EndTag");

            if (String.IsNullOrEmpty(rawValue))
                throw new Exception("SitecoreSpark.CATS.EndTag is empty! Check SitecoreSpark.CATS.Settings.config file.");

            return rawValue;
        }

        /// <summary>
        /// Gets the current context database name. If Sitecore.Context.Database or ContentDatabase is null, returns SitecoreSpark.CATS. from settings.
        /// </summary>
        public static string GetCurrentDatabaseName()
        {
            string databaseName = string.Empty;

            if (Sitecore.Context.Database != null && !Sitecore.Context.Database.Name.Equals("core"))
                return Sitecore.Context.Database.Name;
            else if (Sitecore.Context.ContentDatabase != null)
                return Sitecore.Context.ContentDatabase.Name;
            else
                return Sitecore.Configuration.Settings.GetSetting("SitecoreSpark.CATS.PublishedDatabase");
        }
    }
}