/// <reference path="jquery-1.8.3-vsdoc.js" />
/// <reference path="jquery-1.8.3.js" />

// Get data
function searchFlight(btype, dcity, rcity, ddate, rdate) {
//$(function() {
	// Send an AJAX request
	var departdate = new Date(ddate);
	var returndate = new Date(rdate);
	var paras = {
		bookingtype: btype,
		from: dcity,
		to: rcity,
		departdate: ddate,
		returndate: rdate
	};
	$.ajax({
		type: "GET",
		url: "api/flight/search",
		data: paras,
		dataType: "json",
		success: function(data) {
			if ($.type(data) == "string")
				data = $.parseJSON(data);
			appendContent(data);
		}
	});
}

function appendContent(data) {
	var panel = $("#panel_searched");
	panel.empty();
	var nDirection = data.Data.length;
	for (var i = 0; i < nDirection; i++) {
		var txt = "Các chuyến bay từ " + data.Data[i].DepartCity +
			" đến " + data.Data[i].ReturnCity;
		$("<h2/>", { text: txt }).appendTo(panel);
		$("<p/>", { text: "Ngày: " + data.Data[i].Date }).appendTo(panel);
		var gridid = "grid" + i;
		var radioname = "radioname" + i;
		$("<div/>", { id: "grid" + i , style: "margin: auto;"}).appendTo(panel);
		buildGrid("#" + gridid, data.Data[i].Flights, radioname);
	}
}

function buildGrid(div, data, radioname) {
	//alert(data);
	var source ={
        localdata: data,
        datatype: "array"
    };
	var dataAdapter = new $.jqx.dataAdapter(source, {
		loadComplete: function (data) { },
		loadError: function (xhr, status, error) { }
	});
	$(div).jqxGrid({
		source: dataAdapter,
		sortable: true,
		width: 680,
		autoheight: true,
		//altrows: true,
		//enabletooltips: true,
		columns: [
            {
            	text: 'Hãng bay',
            	datafield: 'AirlineName',
            	cellsalign: 'center',
            	width: 120
            },
			{
				text: 'Mã chuyến bay',
				datafield: 'FlightNo',
				cellsalign: 'center',
				sortable: false,
				width: 120
			},
			{
				text: 'Thời gian bắt đầu',
				datafield: 'StartTime',
				cellsalign: 'center',
				width: 130
			},
			{
				text: 'Thời gian kết thúc',
				datafield: 'EndTime',
				cellsalign: 'center',
				width: 130
			},
			{
				text: 'Giá',
				datafield: 'Price',
				cellsalign: 'right',
				cellsformat: 'd',
				sortable: true,
				width: 80
			},
			{
				text: 'Đơn vị',
				cellsalign: 'center',
				cellsrenderer: function () {
					return "VND";
				},
				sortable: false,
				width: 50
			},
			{
				text: "Chọn",
				cellsrenderer: function (row, columnfield, value, defaulthtml, columnproperties) {
					return "<input type='radio' name='" + radioname + "'>";
				},
				sortable: false,
				cellsalign: 'center',
				width: 50
			}
        ]
	});
}