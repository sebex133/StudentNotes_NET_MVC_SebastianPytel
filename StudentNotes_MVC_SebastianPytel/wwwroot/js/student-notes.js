//refresh notes listing
var actualIndexView = '/StudentNotes/IndexAjaxModified';

function refreshNotesView() {
    //$('#notesAll').html('');
    $('#notesAllLoader').removeClass('d-none');
    setTimeout(function () {
        $('#notesAll').load(actualIndexView, function () {
            $('#notesAllLoader').addClass('d-none');
        });
    }, 300)
}

$(document).on('click', '.ajax-index-sort', function () {
    actualIndexView = $(this).data('sort-url');

    refreshNotesView();
});

//Hide show
$('#studentNotesModal').on('hidden.bs.modal', function (e) {
    $('#studentNotesModal .modal-title').html('');
    $('#studentNotesModal .inside').html('');
    $('#studentNotesModal .modal-loader').addClass('d-none');
});

//Modal show
$('#studentNotesModal').on('show.bs.modal', function (e) {
    const loadUrl = $(e.relatedTarget).data('modal-url');
    const title = $(e.relatedTarget).data('modal-title');

    $('#studentNotesModal .modal-title').html(title);
    $('#studentNotesModal .inside').html('');
    $('#studentNotesModal .modal-loader').removeClass('d-none');

    setTimeout(function () {
        $('#studentNotesModal .inside').load(loadUrl, function () {
            $('#studentNotesModal .modal-loader').addClass('d-none');
        });
    }, 300);
});

$(document).on('click', '.ajax-edit', function () {
    const noteId = $(this).data('id');

    $('#studentNotesModal .inside').html('');
    $('#studentNotesModal .modal-loader').removeClass('d-none');

    setTimeout(function () {
        $('#studentNotesModal .inside').load('/StudentNotes/EditAjax/' + noteId, function () {
            $('#studentNotesModal .modal-loader').addClass('d-none');
        });
    }, 300);
});

//dynamic forms
editingClose = function (xhr) {
    let noteId = xhr.responseJSON.noteId;
    let noteLabel = xhr.responseJSON.noteLabel;
    let status = xhr.responseJSON.success;

    if (status) {
        $('#studentNotesModal .modal-title').html(noteLabel);
        //$('#studentNotesModal .inside').html('Saved successfully note ' + noteLabel + ' reloading note');
        refreshNotesView();

        setTimeout(function () {
            $('#studentNotesModal .modal-loader').addClass('d-none');
            $('#studentNotesModal').modal('hide');
            refreshNotesView();
        }, 1000);
    } else {
        $('#studentNotesModal .modal-title').html(noteLabel);
        $('#studentNotesModal .inside').html('Error ' + noteLabel + ' - reloading note editor');
        refreshNotesView();

        setTimeout(function () {
            //Reload edited note
            $('#studentNotesModal .inside').load('/StudentNotes/EditAjax/' + noteId, function () {
                $('#studentNotesModal .modal-loader').addClass('d-none');
            });
            $('#studentNotesModal .modal-loader').addClass('d-none');
        }, 1000);
    }
}

editingProcess = function (xhr) {
    $('#studentNotesModal .inside').html('Saving note...');
    $('#studentNotesModal .modal-loader').removeClass('d-none');
    $('.submit-ajax-button').attr('disabled', 'disabled');
}

deletedClose = function (xhr) {
    let noteId = xhr.responseJSON.noteId;
    let noteLabel = xhr.responseJSON.noteLabel;

    //$('#studentNotesModal .inside').html('Deleted successfully note ' + noteLabel);

    setTimeout(function () {
        $('#studentNotesModal .modal-loader').addClass('d-none');
        $('#studentNotesModal').modal('hide');
        refreshNotesView();
    }, 1000);
}

deletingProcess = function (xhr) {
    $('#studentNotesModal .inside').html('Deleting note...');
    $('#studentNotesModal .modal-loader').removeClass('d-none');
    $('.submit-ajax-button').attr('disabled', 'disabled');
}

creatingClose = function (xhr) {
    let noteId = xhr.responseJSON.noteId;
    let noteLabel = xhr.responseJSON.noteLabel;
    let status = xhr.responseJSON.success;

    if (status) {
        //$('#studentNotesModal .inside').html('Saved successfully note ' + noteLabel);

        setTimeout(function () {
            $('#studentNotesModal .modal-loader').addClass('d-none');
            $('#studentNotesModal').modal('hide');
            refreshNotesView();
        }, 1000);
    }
    else {
        $('#studentNotesModal .inside').html('Error - ' + noteLabel);

        setTimeout(function () {
            $('#studentNotesModal .inside').load('/StudentNotes/CreateAjax', function () {
                $('#studentNotesModal .modal-loader').addclass('d-none');
            });

            $('#studentNotesModal .modal-loader').addClass('d-none');
        }, 1000);
    }
}

creatingProcess = function (xhr) {
    $('#studentNotesModal .inside').html('Saving note...');
    $('#studentNotesModal .modal-loader').removeClass('d-none');
    $('.submit-ajax-button').attr('disabled', 'disabled');
}