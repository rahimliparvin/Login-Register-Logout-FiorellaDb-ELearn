$(function () {

 
    $(document).on("click", ".deletePhoto", function () {


        let courseImageId = $(this).attr("data-id")

        console.log(courseImageId);

        let deletedElem = $(this)

        let data = { id: courseImageId }

        var url = "/Admin/Course/DeleteCourseImage";
        var xhr = new XMLHttpRequest();
        xhr.open("POST", url, true);
        xhr.send();


        $.ajax({
            url: "/Admin/Course/DeleteCourseImage",
            type: "Post",
            data: data,
            success: function (res) {
                if (res) {

                    $(deletedElem).prev().remove()
                    $(deletedElem).remove()

                } else {

                    alert("Courses images must be min 1")

                }

            }
        })

    })
})
