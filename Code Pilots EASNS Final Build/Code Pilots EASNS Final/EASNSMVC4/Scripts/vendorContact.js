/*MAREK GRABOWIECKI VENDOR CONTACT JS*/
$(function()
{

	/*The Submit button Click event */
	$("#contactSubmit").click(function()
	{
		var name = $("#coName").val();
		var rep = $("#repName").val();
		var subj = $("#subj").val();
		var coEmail = $("#coEmail").val();
		var repEmail = $("#repEmail").val();
		var det =$("#details").val();

		if(name == "")
			$("#coName").addClass('redBorder');

		if(rep == "")
			$("#repName").addClass('redBorder');

		if(subj == "")
			$("#subj").addClass('redBorder');

		if(coEmail == "")
			$("#coEmail").addClass('redBorder');

		if(repEmail == "")
			$("#repEmail").addClass('redBorder');

		if(det == "")
			$("#details").addClass('redBorder');

		if((name != "") && (rep != "") && (subj != "") && (coEmail != "") && (repEmail != "") && (det != ""))
		{
			$("#failure").css('display', 'none');
			$("#vendorForm").css('display', 'none');
			$("#confirmation").fadeIn(1000);
		}

		else
		{
			$("#failure").fadeIn(100);
		}
	});

	/*Removing the red border if textbox is focused*/
	$("#coName").click(function()
	{
		$(this).removeClass('redBorder');
	});

	$("#repName").click(function()
	{
		$(this).removeClass('redBorder');
	});

	$("#subj").click(function()
	{
		$(this).removeClass('redBorder');
	});

	$("#coEmail").click(function()
	{
		$(this).removeClass('redBorder');
	});

	$("#repEmail").click(function()
	{
		$(this).removeClass('redBorder');
	});

	$("#details").click(function()
	{
		$(this).removeClass('redBorder');
	});
});