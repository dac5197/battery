﻿window.bootstrapInteropt = {
    showModal: id => {
        $(`#${id}`).modal('show');
    },
    closeModal: id => {
        $(`#${id}`).modal('hide');
    }
};