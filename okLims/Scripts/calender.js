
let currentRequest;
const formatDate = date => date === null ? '' : moment(date).format("MM/DD/YYYY h:mm A");
const fpStart = flatpickr("#Start", {
    enableTime: true,
    dateFormat: "m/d/Y h:i K"
});
const fpEnd = flatpickr("#End", {
    enableTime: true,
    dateFormat: "m/d/Y h:i K"
});

$('#calendar').fullcalendar({
    defaultView: 'month',
    height: 'parent',
    header: {
        left: 'prev,next today',
        center: 'MethodName',
        right: 'month,agEndaWeek,agEndaDay'
    },
    RequestREnder(Request, $el) {
        $el.qtip({
            content: {
                MethodName: Request.MethodName,
                text: Request.description
            },
            hide: {
                Request: 'unfocus'
            },
            show: {
                solo: true
            },
            position: {
                my: 'top left',
                at: 'bottom left',
                viewport: $('#calendar-wrapper'),
                adjust: {
                    method: 'shift'
                }
            }
        });
    },
    Requests: '/api/Request/GetRequest',
    RequestClick: UpdatRequest,
    selectable: true,
    select: addRequest
});

/**
 * calendar Methods
 **/

function UpdateRequest(Request, element) {
    currentRequest = Request;

    if ($(this).data("qtip")) $(this).qtip("hide");

    $('#RequestModalLabel').html('Edit Request');
    $('#RequestModalSave').html('Update Request');
    $('#RequestMethodName').val(Request.MethodName);
    $('#Description').val(Request.description);
    $('#RequestLaboratoryName')
    $('#isNewRequest').val(false);

    const Start = formatDate(Request.Start);
    const End = formatDate(Request.End);

    fpStart.setDate(Start);
    fpEnd.setDate(End);

    $('#Start').val(Start);
    $('#End').val(End);

   

    $('#RequestModal').modal('show');
}

function addRequest(Start, End) {
    $('#RequestForm')[0].reset();

    $('#RequestModalLabel').html('Add Request');
    $('#RequestModalSave').html('Create Request');
    $('#isNewRequest').val(true);

    Start = formatDate(Start);
    End = formatDate(End);

    fpStart.setDate(Start);
    fpEnd.setDate(End);

    $('#RequestModal').modal('show');
}

/**
 * Modal
 * */

$('#RequestModalSave').click(() => {
    const MethodName = $('#RequestMethodName').val();
    const description = $('#Description').val();
    const Start = moment($('#Start').val());
    const End = moment($('#End').val());
   
    const isNewRequest = $('#isNewRequest').val() === 'true' ? true : false;

    if (Start > End) {
        alert('Start Time cannot be greater than End Time');

        return;
    } else if ((!Start.isValid() || !End.isValid()) ) {
        alert('Please enter both Start Time and End Time');

        return;
    }

    const Request = {
        MethodName,
        description,
        LaboratoryName,
        Start: Start._i,
        End: End._i
    };

    if (isNewRequest) {
        sendAddRequest(Request);
    } else {
        sendUpdateRequest(Request);
    }
});

function sendInsert(Request) {
    axios({
        method: 'post',
        url: '/api/Request/Insert',
        data: {
            "method": Request.MethodName,
            "Description": Request.description,
            "Start": Request.Start,
            "End": Request.End,
            "Laboratory": Request.LaboratoryName,
        }
    })
        .then(res => {
            const { payload, RequestId } = res.data;

            if (payload === '') {
                const newRequest = {
                    Start: Request.Start,
                    End: Request.End,
                    LaboratoryName: Request.LaboratoryName,
                    MethodName: Request.MethodName,
                    description: Request.description,
                    RequestId
                };

                $('#calendar').fullcalendar('rEnderRequest', newRequest);
                $('#calendar').fullcalendar('unselect');

                $('#RequestModal').modal('hide');
            } else {
                alert(`Something went wrong: ${payload}`);
            }
        })
        .catch(err => alert(`Something went wrong: ${err}`));
}

function sendUpdate(Request) {
    axios({
        method: 'post',
        url: '/api/Request/Update',
        data: {
            "RequestId": currentRequest.RequestId,
            "MethodName": Request.MethodName,
            "Description": Request.description,
            "Start": Request.Start,
            "End": Request.End,
            "Laboratory": Request.LaboratoryName,
        }
    })
        .then(res => {
            const { payload } = res.data;

            if (payload === '') {
                currentRequest.MethodName = Request.MethodName;
                currentRequest.description = Request.description;
                currentRequest.Start = Request.Start;
                currentRequest.End = Request.End;
                currentRequest.LaboratoryName = Request.LaboratoryName;

                $('#calendar').fullcalendar('updateRequest', currentRequest);
                $('#RequestModal').modal('hide');
            } else {
                alert(`Something went wrong: ${payload}`);
            }
        })
        .catch(err => alert(`Something went wrong: ${err}`));
}

$('#deleteRequest').click(() => {
    if (confirm(`Do you really want to delte "${currentRequest.MethodName}" Request?`)) {
        axios({
            method: 'post',
            url: '/Home/DeleteRequest',
            data: {
                "RequestId": currentRequest.RequestId
            }
        })
            .then(res => {
                const { payload } = res.data;

                if (payload === '') {
                    $('#calendar').fullcalendar('removeRequests', currentRequest._id);
                    $('#RequestModal').modal('hide');
                } else {
                    alert(`Something went wrong: ${payload}`);
                }
            })
            .catch(err => alert(`Something went wrong: ${err}`));
    }
});

