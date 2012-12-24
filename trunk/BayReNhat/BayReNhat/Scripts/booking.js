var params = {};
var bookingType;

function passOnewaypParam(bType, from, to, dfno, ddate) {
	delete params;
	params = {};
	bookingType = bType;
	params.from = from;
	params.to = to;
	params.departDate = ddate;
	params.departFlightNo = dfno;
}

function passRoundtripParam(bType, from, to, dfno, ddate, rfno, rdate) {
	delete params;
	params = {};
	bookingType = bType;
	params.from = from;
	params.to = to;
	params.departDate = ddate;
	params.returnDate = rdate;
	params.departFlightNo = dfno;
	params.returnFlightNo = rfno;
}

$("#btnRegister").click(function () {
	if (validation() == true) {
		params.firstName = $("#firstName").val();
		params.lastName = $("#lastName").val();
		params.tel = $("#tel").val();
		params.email = $("#email").val();
		var url;
		if (bookingType == "oneway")
			url = "api/flight/onewayregister";
		else {
			url = "api/flight/roundtripregister";
		}
		$.ajax({
			url: url,
			type: "GET",
			data: params,
			dataType: "JSON",
			success: function (response) {
				alert(response);
			}
		});
	}
	return false;
});

function validation(parameters) {
	var err = false;
	$("#error").empty();
	if ($("#lastName").val() == "") {
		$("#error").append($("<li/>", { text: "Bạn phải điền họ" }));
		err = true;
	}
	if ($("#firstName").val() == "") {
		$("#error").append($("<li/>", { text: "Bạn phải điền tên" }));
		err = true;
	} 
	if ($("#tel").val() == "") {
		$("#error").append($("<li/>", { text: "Bạn phải điền số điện thoại" }));
		err = true;
	} else {
		var phoneReg = new RegExp("^[0-9]{10,11}$");
		if (!phoneReg.test($("#tel").val())) {
			$("#error").append($("<li/>", { text: "Số điện thoại không hợp lệ" }));
			err = true;
		}
	}
	if ($("#email").val() == "") {
		$("#error").append($("<li/>", { text: "Bạn phải điền email" }));
		err = true;
	} else {
		var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
		if (!filter.test($("#email").val())) {
			$("#error").append($("<li/>", { text: "Địa chỉ email không hợp lệ" }));
			err = true;
		}
	}
	return !err;
}