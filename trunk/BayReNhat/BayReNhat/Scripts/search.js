/// <reference path="jquery-1.8.3-vsdoc.js" />
/// <reference path="jquery-1.8.3.js" />
/// <reference path="util.js" />
var searchResult = {};
var nDirection = 0;
var flightName = "radioname";

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
		success: function (data) {
			if ($.type(data) == "string")
				data = $.parseJSON(data);
			searchResult = data;
			appendContent(data);
		}
	});
}

function appendContent(data) {
	var panel = $("#panel_searched");
	panel.empty();
	nDirection = data.Data.length;
	if (nDirection != 0) {
		for (var i = 0; i < nDirection; i++) {
			var txt = "Các chuyến bay từ " + getCityNameByCode(data.Data[i].DepartCity) +
				" đến " + getCityNameByCode(data.Data[i].ReturnCity);
			$("<h2/>", { text: txt }).appendTo(panel);
			$("<p/>", { text: "Ngày: " + data.Data[i].Date, style: "font-weight: bold" }).appendTo(panel);
			var gridid = "grid" + i;
			//var radioname = flightName + i;
			$("<div/>", { id: "grid" + i, style: "margin: auto;" }).appendTo(panel);
			buildGrid("#" + gridid, data.Data[i].Flights, i);
		}
		
//		var form = $("<form/>", {
//			action: "Booking",
//			method: "POST",
//			id: "formBooking",
//			style: "border: none"
//		});
//		form.append($("<input/>", {
//			type: 'submit',
//			value: 'Đặt vé',
//			class: 'button',
//			//formaction: '#',
//			id: 'btnBooking',
//			style: 'margin-top: 30px;'
//		}));
//		$(panel).append(form);
//		$("#formBooking").submit(beforeSubmit());
		$("#panel_searched").append($("<button/>", { text: "Đặt vé", class: "button", id: "btnBooking", onclick: "testfn()" }));
	} else {
		var txt = "Xin lỗi! Chúng tôi không tìm thấy chuyến bay nào theo yêu cầu của bạn.";
		$("<p/>", {text: txt, style: 'color: red'}).appendTo(panel);
	}
}

function buildGrid(div, data, radioname) {
	//alert(data);
	var source = {
		localdata: data,
		datatype: "array"
	};
	var dataAdapter = new $.jqx.dataAdapter(source, {
		loadComplete: function(data) {
		},
		loadError: function(xhr, status, error) {
		}
	});
	$(div).jqxGrid({
		source: dataAdapter,
		sortable: true,
		width: 680,
		autoheight: true,
		rowsheight: 35,
		//altrows: true,
		//enabletooltips: true,
		columns: [
			{
				text: 'Logo',
				align: 'center',
				cellsalign: 'center',
				width: 125,
				cellsrenderer: function(row, columnfield, value, defaulthtml, columnproperties) {
					var img = "/Images/airmekong_logo.png";
					if (source.localdata[row].AirlineName == "Vietnam Airline")
						img = "/Images/vnairline_logo.png";
					else if (source.localdata[row].AirlineName == "Jesta")
						img = "/Images/jetstar_logo.png";
					return $(defaulthtml).append($("<img/>", { src: img, height: '35px', width: '124px' })).prop("outerHTML");
				}
			},
			{
				text: 'Hãng bay',
				datafield: 'AirlineName',
				cellsalign: 'center',
				align: 'center',
				width: 120
			},
			{
				text: 'Mã chuyến bay',
				datafield: 'FlightNo',
				cellsalign: 'center',
				align: 'center',
				sortable: false,
				width: 120
			},
			{
				text: 'Thời gian',
				datafield: 'StartTime',
				cellsalign: 'center',
				align: 'center',
				width: 130,
				cellsrenderer: function(row, columnfield, value, defaulthtml, columnproperties) {
					return $(defaulthtml).append(" - " + source.localdata[row].EndTime).prop("outerHTML");
				}
			},
			{
				text: 'Giá',
				datafield: 'Price',
				cellsalign: 'right',
				align: 'center',
				cellsformat: 'd',
				sortable: true,
				cellsrenderer: function(row, columnfield, value, defaulthtml, columnproperties) {
					return $(defaulthtml).html($.number(value) + " VNĐ").prop("outerHTML");
				},
				width: 120
			},
			{
				text: "Chọn",
				cellsrenderer: function(row, columnfield, value, defaulthtml, columnproperties) {
					return $(defaulthtml).append($("<input/>", { type: "radio", name: radioname, id: row })).prop("outerHTML");
				},
				sortable: false,
				cellsalign: 'center',
				align: 'center'
			}
		]
	});
	$(div).bind("rowselect", function(event) {
		//alert("hello");
		$("#" + radioname + event.args.rowindex).attr("checked", true);
	});
}

// Before form submit
function beforeSubmit() {
	var flights = [{
		AirlineName: "dfasdfa",
		FlightNo: "dasdf",
		Date: "12/23/2012",
		StartTime: "6:00",
		EndTime: "9:00",
		Price: 1000000
	},
	{
		AirlineName: "afsdf",
		FlightNo: "dafs",
		Date: "12/22/2012",
		StartTime: "8:00",
		EndTime: "17:00",
		Price: 102039
	}];
	
	$('<input/>').attr('type', 'hidden')
		.attr('name','flights')
		.attr('value', JSON.stringify(flights))
		.appendTo($("#formBooking"));

	return true;
}

function testfn() {
	if ($(":checked").length != nDirection) {
		alert("Bạn phải chọn " + nDirection + " chuyến bay!");
		return false;
	}
	var data = [];
	$(":checked").each(function (i, flight) {
		var x = $(flight).attr("name"), y = $(flight).attr("id");
		data.push({
			AirlineName: searchResult.Data[x].Flights[y].AirlineName,
			From: searchResult.Data[x].DepartCity,
			To: searchResult.Data[x].ReturnCity,
			FlightNo: searchResult.Data[x].Flights[y].FlightNo,
			Date: searchResult.Data[x].Date,
			StartTime: searchResult.Data[x].Flights[y].StartTime,
			EndTime: searchResult.Data[x].Flights[y].EndTime,
			Price: searchResult.Data[x].Flights[y].Price
		});
	});
	$.ajax({
		url: "booking",
		type: "POST",
		data: JSON.stringify({ flights: data, bookingType: searchResult.BookingType }),
		dataType: "json",
		contentType: "application/json",
		success: function (result) {
			//alert(result);
			window.location = result.redirect;
		}
	});
};