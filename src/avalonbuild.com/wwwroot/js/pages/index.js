$(function () {

    addLoadEvent(preloader);

    $("#contact-submit").click(postContact);

    $("#scroller").click(function (e){e.preventDefault();
        $('html, body').animate({
            scrollTop: $("#testimonials").offset().top
        }, 800);
    });

});

function preloader() {

	if (document.images) {

		var img1 = new Image();
		var img2 = new Image();
		var img3 = new Image();
        var img4 = new Image();
        var img5 = new Image();

		// img1.src = "/images/gallery/b55.jpg";
		// img2.src = "/images/gallery/b38.jpg";
		// img3.src = "/images/gallery/b47.jpg";
        // img4.src = "/images/gallery/b25-1.jpg";

		img1.src = "/images/gallery/206 Tower Angle Front For N2 Print Ad 2.jpg";
		img2.src = "/images/gallery/Dining reduced.jpg";
		img3.src = "/images/gallery/avalon 3_-22.jpg";
        img4.src = "/images/gallery/avalon 3_-13.jpg";
        img5.src = "/images/gallery/_DSF6212.jpg";

	}
}

function addLoadEvent(func) {
	var oldonload = window.onload;
	if (typeof window.onload != 'function') {
		window.onload = func;
	} else {
		window.onload = function() {
			if (oldonload) {
				oldonload();
			}
			func();
		}
	}
}



function postContact() {

    $link = $(this);
    $text = $link.html();

    $link.attr('disabled', 'disabled');
    $link.html("Sending ...");

    dataString = $("#contact").find("form").serialize();

    $.ajax({
        url: "/contact",
        type: 'POST',
        data: dataString,
        success: function (result) {
            $notification = $("#contact-notification");
            $notification.html(result);
            $notification.addClass("show");
            $notification.addClass("success");
            $notification.removeClass("error");
        },
        error: function (xhr, status, error) {
            $notification = $("#contact-notification");
            $notification.html(xhr.responseText);  //xhr.statusText would show "Bad Request" or "Internal Server Error"
            $notification.addClass("show");
            $notification.addClass("error");
            $notification.removeClass("success");
        },
        complete: function () {
            $link.removeAttr('disabled');
            $link.html($text);
        }
    });

    return false;
}