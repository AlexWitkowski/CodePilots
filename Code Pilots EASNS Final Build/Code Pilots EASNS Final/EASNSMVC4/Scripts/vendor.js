$(function()
{
	$("#mapFrenchSelect").click(function()
	{
		$("#englishMap").css('display', 'none');
		$("#nonProfitMap").css('display', 'none');
		$("#frenchMap").fadeIn(1000);
	});

	$("#mapEnglishSelect").click(function()
	{
		$("#frenchMap").css('display', 'none');;
		$("#nonProfitMap").css('display', 'none');;
		$("#englishMap").fadeIn(1000);
	});

	$("#mapNonProfit").click(function()
	{
		$("#englishMap").css('display', 'none');;
		$("#frenchMap").css('display', 'none');;
		$("#nonProfitMap").fadeIn(1000);
	});
});