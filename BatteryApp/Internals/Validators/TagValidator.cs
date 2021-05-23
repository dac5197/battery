using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.TagModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Internals.Validators
{
    public class TagValidator : BaseValidator, ITagValidator
    {
        private readonly ITagController _tagController;

        public TagValidator(ITagController tagController)
        {
            _tagController = tagController;
        }

        public async Task<Dictionary<string, List<string>>> ValidateAsync(Tag tag, Battery battery)
        {
            ClearErrors();

            List<string> messages = new();

            var existingTags = await _tagController.GetAllTagsAsync(battery);

            messages.Add(ValidateTag_DuplicateName(tag, existingTags));

            AddToErrors("Tag", messages);

            return _errors;
        }

        private static string ValidateTag_DuplicateName(Tag tag, List<Tag> existingTags)
        {
            string tempMessage = "";

            if (existingTags.Where(x => x.Name == tag.Name).Any())
            {
                tempMessage = "Duplicate Tag Name. Please enter a new Name or select an existing Tag.";
            }

            return tempMessage;
        }
    }
}
