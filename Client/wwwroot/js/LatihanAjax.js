console.log("Testingg");

//$("h1").html("berubah coy");

//$(".test").html("berubah coy 2")

//$("#test2").html("berubah coy 3")

//ajax Consume API Sederhana
$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/", //OpenAPI
    //success: function (result) {
        //console.log(result.results);
    //    var temp = "";

      //  $.each(result.results, (key,val) => {
        //    temp += "<li>"+val.name+"</li>";
        //})
        //console.log(temp);
        //$("#listPoke").html(temp);
    //}

}).done((result) => {
    console.log(result.results);
    var temp = "";

        $.each(result.results, (key,val) => {
            temp += `<tr>
                        <td>${key+1}</td>
                        <td>${val.name}</td>
                        <td><button type="button" class="btn btn-primary" onclick="detail('${val.url}')" data-bs-toggle="modal" data-bs-target="#modalPoke">Detail</button></td>
                    </tr>`;
        })
        console.log(temp);
        $("#tbodyPoke").html(temp);
}).fail((error) => {
    console.log(error);
})

//ready function
$(document).ready(function () {
    $('#tablePokemon').DataTable({
        ajax: {
            url: "https://pokeapi.co/api/v2/pokemon/",
            dataSrc: 'results'
        },
        columns: [
            {
                data: "name",
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { data: "name" },
            {
                data: "name",
                render: function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" onclick="detail('${row.url}')" data-bs-toggle="modal" data-bs-target="#modalPoke">Detail</button>`;
                }
            }
        ]
    });
});



function detail(stringUrl) {
    $.ajax({
        url: stringUrl
    }).done((res) => {
        console.log(res);
        $("#modalTitle").html("Detail Pokemon");
        $("#pokeImage").attr("src", res.sprites.other.dream_world.front_default);
        $("#pokeName").html(res.name);
        $("#pokeTypes").html(getTypesHtml(res.types));
        $("#statsList").html(getStatsList(res.stats));
        $("#movesList").html(getMovesList(res.moves));
        renderStatsProgress(res.stats);
    });
}

function getTypesHtml(types) {
    return types.map(type => getBadgeClass(type.type.name)).join('');
}

function getBadgeClass(typeName) {
    switch (typeName) {
        case 'fire':
            return '<span class="badge bg-danger">Fire</span>';
        case 'grass':
            return '<span class="badge bg-success">Grass</span>';
        case 'flying':
            return '<span class="badge bg-info text-dark">Flying</span>';
        case 'poison':
            return '<span class="badge bg-warning text-dark">Poison</span>';
        case 'water':
            return '<span class="badge bg-primary">Water</span>';
        case 'bug':
            return '<span class="badge bg-success">Bug</span>';
        case 'normal':
            return '<span class="badge bg-secondary">Normal</span>';
        default:
            return '';
    }
}

function getStatsList(stats) {
    let statsList = '';
    for (let i = 0; i < stats.length; i++) {
        statsList += `<li>${stats[i].stat.name}: <div class="progress" style="height: 20px;">
                            <div class="progress-bar" role="progressbar" style="width: ${stats[i].base_stat}%; background-color: rgba(54, 162, 235, 0.5);" aria-valuenow="${stats[i].base_stat}" aria-valuemin="0" aria-valuemax="100"></div>
                        </div></li>`;
    }
    return statsList;
}

function getMovesList(moves) {
    let movesList = '';
    for (let i = 0; i < 6 && i < moves.length; i++) {
        movesList += `<li>${moves[i].move.name}</li>`;
    }
    return movesList;
}

function renderStatsProgress(stats) {
    for (let i = 0; i < stats.length; i++) {
        const baseStat = stats[i].base_stat;
        $(`#statValue${i}`).wrap(`<div class="progress" style="height: 20px;"></div>`);
        $(`#statValue${i}`).after(`
            <div class="progress-bar" role="progressbar" style="width: ${baseStat}%; background-color: rgba(54, 162, 235, 0.5);" aria-valuenow="${baseStat}" aria-valuemin="0" aria-valuemax="100"></div>
        `);
    }
}