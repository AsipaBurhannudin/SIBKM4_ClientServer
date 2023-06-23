$.ajax({
    url: "https://localhost:7062/api/Employee"
}).done((result) => {
    console.log(result);
    var temp = "";

    $.each(result, (key, val) => {
        temp += `<tr>
                    <td>${key + 1}</td>
                    <td>${val.name}</td>
                    <td><button type="button" class="btn btn-primary" onclick="detail('${val.url}')" data-bs-toggle="modal" data-bs-target="#modalEmp">Detail</button></td>
                </tr>`;
    });

    console.log(temp);
    $("#tbodyEmployee").html(temp);
}).fail((error) => {
    console.log(error);
});


$(document).ready(function () {
    $('#tblEmployee').DataTable({
        ajax: {
            url: "https://localhost:7062/api/Employee",
            dataSrc: 'results'
        },
        columns: [
            {
                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { data: "NIK" },
            { data: "FirstName" },
            { data: "LastName" },
            { data: "BirthDate" },
            { data: "Gender" },
            { data: "Hiring" },
            { data: "Email" },
            { data: "Phone" },
            {
                data: null,
                render: function (data, type, row) {
                    return '<button type="button" class="btn btn-primary" onclick="detail(\'' + row.url + '\')" data-bs-toggle="modal" data-bs-target="#modalEmp">Detail</button>';
                }
            }
        ]
    });
});