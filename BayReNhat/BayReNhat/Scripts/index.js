$(function () {
	init();
	registerdlg("#dlgCity", "#departcity");
	registerdlg("#dlgCity", "#returncity");
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