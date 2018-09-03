using Sitecore.Data.Items;
using Sitecore.Data.Validators;
using SitecoreSpark.CATS.Infrastructure;
using SitecoreSpark.CATS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SitecoreSpark.CATS.Validators
{
    [Serializable]
    public class NoDuplicateTokens : StandardValidator
    {
        public NoDuplicateTokens() { }

        public NoDuplicateTokens(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string Name
        {
            get
            {
                return "No Duplicate Tokens";
            }
        }

        protected override ValidatorResult Evaluate()
        {
            string inputValue = this.ControlValidationValue.Trim();
            Item currentItem = this.GetItem();

            // Load all existing Tokens
            IEnumerable<Item> libraries = TokenService.GetAllTokenLibraries();
            IEnumerable<ContentToken> tokens = TokenService.GetTokensFromLibraries(libraries);

            // Check for duplicate key
            if (tokens.Any(u => u.Pattern.Equals(inputValue, StringComparison.InvariantCultureIgnoreCase) && u.ItemID != currentItem.ID.Guid))
            {
                this.Text = $"A token already exists with this pattern. Please use a unique value.";
                return this.GetFailedResult(ValidatorResult.FatalError);
            }

            return ValidatorResult.Valid;
        }

        protected override ValidatorResult GetMaxValidatorResult()
        {
            throw new NotImplementedException();
        }
    }
}