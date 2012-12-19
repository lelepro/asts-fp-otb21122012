/// <reference path="jquery-1.8.3.js" />
/// <reference path="jquery-1.8.3-vsdoc.js" />

$(function () {
	init();
	registerdlg("#dlgCity", "#departcity");
	registerdlg("#dlgCity", "#returncity");
	$("#returnday").attr("disabled", $("#rdb_roundtrip").val());
	$("input[name=bookingtype]:radio").change(function() {
		if ($("input[name=bookingtype]:checked").val() == "oneway")
			$("#returnday").attr("disabled", true);
		else if ($("input[name=bookingtype]:checked").val() == "roundtrip")
			$("#returnday").attr("disabled", false);
	});
});

function init() {
	$("#departday").datepicker({minDate: 0});
	$("#returnday").datepicker({minDate: 0 });
	$("#dlgCity").dialog({
		autoOpen: false,
		draggable: false,
		resizable: false,
	});
	$("#dlgCity").selectable({
		filter: "li",
		selected: function (event, ui) {
			$(ui.selected).addClass("ui-selected").siblings().removeClass("ui-selected");
		}
	});
}

function registerdlg(dlgId, targetId) {
	$(targetId).click(function () {
		$(dlgId).selectable("option", {
			stop:
				function() {
					$(targetId).val($(".ui-selected").text());
				}
		});
		$(dlgId).dialog("option", {
			position: { my: "left top", at: "left bottom", of: targetId },
			open: function() {
				var selected = $(".ui-widget-content .ui-selected");
				if (selected != null) {
					selected.removeClass("ui-selected");
				}
			}
		});
		$(dlgId).dialog("open");
		$(targetId).focus();
	});
	$(targetId).blur(function () {
		$(dlgId).dialog("close");
	});
}

function hintTextbox(textboxId, hint) {
	var textbox = $(textboxId);
	textbox.val(hint);
	textbox.css("color", "gray");
	textbox.attr("isselected", false);
	
	textbox.change(function() {
		if (textbox.val() == hint) {
			textbox.val("");
			textbox.css("color", "");
			textbox.attr("isselected", true);
		} else if (textbox.val() == "") {
			textbox.val(hint);
			textbox.css("color", "gray");
			textbox.attr("isselected", false);
		}
	});
}

// Validation
$(function() {
	$("#btnsearch").click(function() {
		$(".error").empty();
		var err = false;
		if ($("#departcity").val() == "") {
			showError("Bạn chưa chọn điểm đi");
			err = true;
		}
		if ($("#returncity").val() == "") {
			showError("Bạn chưa chọn điểm đến");
			err = true;
		}
		if ($("#departday").val() == "") {
			showError("Bạn chưa chọn ngày đi");
			err = true;
		}
		if ($("#returnday").val() == "" && !$("#returnday").prop("disabled")) {
			showError("Bạn chưa chọn ngày về");
			err = true;
		}
		var deparday = Date.parse($("#departday").val());
		var returnday = Date.parse($("#returnday").val());
		if (deparday != "NaN" && returnday != "NaN" && deparday > returnday) {
			showError("Ngày về phải sau ngày đi");
			err = true;
		}
			
		return !err;
	});
});

function showError(msg) {
	$(".error").append("<li>"+msg+"</li>");
}