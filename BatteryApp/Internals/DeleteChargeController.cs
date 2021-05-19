using BatteryApp.Models.ChargeModel;
using BatteryApp.Models.NoteModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Internals
{
    public class DeleteChargeController : IDeleteChargeController
    {
        private readonly IChargeService _chargeService;
        private readonly INoteService _noteService;
        private readonly ITagController _tagController;

        public DeleteChargeController(IChargeService chargeService, INoteService noteService, ITagController tagController)
        {
            _chargeService = chargeService;
            _noteService = noteService;
            _tagController = tagController;
        }

        public async Task DeleteChargeAndAllRelatedItemsAsync(Charge charge)
        {
            await DeleteChildrenAsync(charge);
            await DeleteChargeOnlyAsync(charge);
        }

        public async Task DeleteChildrenAsync(Charge charge)
        {
            var children = await _chargeService.GetChildren(charge);

            foreach (var child in children)
            {
                await DeleteChargeOnlyAsync(child);
            }
        }

        public async Task DeleteChargeOnlyAsync(Charge charge)
        {
            await DeleteTagRelationshipsAsync(charge.Id);
            //await _noteService.RemoveChildParentHistoryNote(charge); TODO FIX ME
            await _chargeService.Delete(charge.Id);

        }

        public async Task DeleteTagRelationshipsAsync(int chargeId)
        {
            await _tagController.DeleteAllRelationshipsForChargeAsync(chargeId);
        }
    }
}
