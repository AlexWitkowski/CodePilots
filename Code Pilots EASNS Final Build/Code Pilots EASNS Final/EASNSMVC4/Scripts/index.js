$(function()
{
	setTimeout(change1, 5000);
});

function change1()
{
	$("#img1").fadeOut(500);
	$("#bannerLink1").fadeOut(500);
	setTimeout(change2, 7000);
}

function change2()
{
	$("#img2").fadeOut(500);
	$("#bannerLink2").fadeOut(500);
	setTimeout(change3, 7000);
}

function change3()
{
	$("#img1").fadeIn(500);
	$("#img2").fadeIn(4000);
	$("#bannerLink1").fadeIn(500);
	$("#bannerLink2").fadeIn(4000);
	setTimeout(change1, 7000);
}