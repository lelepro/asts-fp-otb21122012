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
	if (nDirection != 0) {
		for (var i = 0; i < nDirection; i++) {
			var txt = "Các chuyến bay từ " + getCityNameByCode(data.Data[i].DepartCity) +
				" đến " + getCityNameByCode(data.Data[i].ReturnCity);
			$("<h2/>", { text: txt }).appendTo(panel);
			$("<p/>", { text: "Ngày: " + data.Data[i].Date, style: "font-weight: bold" }).appendTo(panel);
			var gridid = "grid" + i;
			var radioname = "radioname" + i;
			$("<div/>", { id: "grid" + i, style: "margin: auto;" }).appendTo(panel);
			buildGrid("#" + gridid, data.Data[i].Flights, radioname);
		}
		$(panel).append($("<button/>", {
			text: 'Đặt vé',
			class: 'button',
			formaction: '#',
			id: 'btnBooking',
			style: 'margin-top: 30px'
		}));
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
				cellsrenderer: function (row, columnfield, value, defaulthtml, columnproperties) {
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
				cellsrenderer: function (row, columnfield, value, defaulthtml, columnproperties) {
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
				cellsrenderer: function (row, columnfield, value, defaulthtml, columnproperties) {
					return $(defaulthtml).html($.number(value) + " VNĐ").prop("outerHTML");
				},
				width: 120
			},
			{
				text: "Chọn",
				cellsrenderer: function (row, columnfield, value, defaulthtml, columnproperties) {
					return $(defaulthtml).append($("<input/>", { type: "radio", name: radioname, id: radioname+row })).prop("outerHTML");
				},
				sortable: false,
				cellsalign: 'center',
				align: 'center'
			}
		]
	});
	$(div).bind("rowselect", function (event) {
		//alert("hello");
		$("#" + radioname + event.args.rowindex).attr("checked", true);
	});
}

var cityCode = {
	HAN: "Hà Nội",
	HPH: "Hải Phòng",
	DIN: "Điện Biên",
	VII: "Vinh",
	HUI: "Huế",
	VHD: "Đồng Hới",
	DAD: "Đà Nẵng",
	PXU: "Pleiku",
	TBB: "Tuy Hòa",
	SGN: "Hồ Chí Minh",
	NHA: "Nha Trang",
	DLI: "Đà Lạt",
	PQC: "Phú Quốc",
	VCL: "Tam Kỳ",
	UIH: "Qui Nhơn",
	VCA: "Cần Thơ",
	VCS: "Côn Đảo",
	BMV: "Ban Mê Thuột",
	VKG: "Rạch Giá",
	CAH: "Cà Mau"
};

function getCityNameByCode(city) {
	return cityCode[city];
}