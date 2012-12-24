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

function testajax(url) {
	$.ajax({
		type: "get",
		url: url,
		dataType: "json",
		success: function (response) {
			window.location.href = response.redirect;
		}
	});
}