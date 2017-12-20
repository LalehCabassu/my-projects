$(function(){
	
	$(":button").on("click", function(){
		var name = $(this).attr("name");
		$("#imageIndex").val(name);
		$("#mainForm").submit();
	});
	
//	// Prevent the form from submitting
//	$( "#mainForm" ).submit(function( event ) {
//	  event.preventDefault();
//	});
	
});


//setImageIndex(index)
//{
//	if(index != null)
//	{
//		var x = document.getElementById("imageIndex");
//		x.value = index;
//		var f = document.getElementById("Image List Form");
//		f.submit();
//		return true;
//	}
//	else
//	 	return false;
//}