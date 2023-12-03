function Approvel(Url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#151515',
        cancelButtonColor: '#d9534f',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: Url,
                type: 'Put',
                success: (data) => {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}