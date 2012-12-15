﻿$(function () {
	init();
	registerdlg("#dlgCity", "#departcity");
	registerdlg("#dlgCity", "#returncity");
	hintTextbox("#departcity", "bạn đi từ đâu");
	hintTextbox("#returncity", "bạn muốn đến đâu");
	hintTextbox("#departday", "ngày nào bạn đi");
	hintTextbox("#returnday", "ngày nào bạn về");
	$("input[name=direction]:radio").change(function() {
		if ($("input[name=direction]:checked").val() == "onewaytravel")
			$("#returnday").attr("disabled", true);
		else if ($("input[name=direction]:checked").val() == "returntravel")
			$("#returnday").attr("disabled", false);
	});
});

function init() {
	$("#departday, #returnday").datepicker();
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
	
	textbox.click(function() {
		if (textbox.val() == hint) {
			textbox.val("");
			textbox.css("color", "");
			textbox.attr("isselected", true);
		}
	});
	textbox.change(function() {
		if (textbox.val() == "") {
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
		if ($("#departcity").attr("isselected") == "false") {
			showError("Bạn chưa chọn điểm đi");
			err = true;
		}
		if ($("#returncity").attr("isselected") == "false") {
			showError("Bạn chưa chọn điểm đến");
			err = true;
		}
		if ($("#departday").attr("isselected") == "false") {
			showError("Bạn chưa chọn ngày đi");
			err = true;
		}
		if ($("#returnday").attr("isselected") == "false" && 
			$("#returnday").attr("disabled")) {
			showError("Bạn chưa chọn ngày về");
			err = true;
		}
		return !err;
	});
});

function showError(msg) {
	$(".error").append("<li>"+msg+"</li>");
}