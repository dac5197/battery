////window.bootstrapInteropt = {
////    showModal: id => {
////        $(`#${id}`).modal('show');
////    },
////    closeModal: id => {
////        $(`#${id}`).modal('hide');
////    }
////};

function ShowModal(modalId) {
    $('#' + modalId).modal('show');
}
function CloseModal(modalId) {
    $('#' + modalId).modal('hide');
}