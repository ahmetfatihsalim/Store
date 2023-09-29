var productDataTable;

$(function () {
    loadProductDataTable();
})

function loadProductDataTable() {
    productDataTable = $('#productTable').DataTable({
        "ajax": { url: 'Product/GetAll' }, /* we might need more properties. thats why this is an object*/
        "columns": [
            { data: 'title', "width": "20%" },
            { data: 'isbn', "width": "10%" },
            { data: 'price', "width": "10%" },
            { data: 'author', "width": "20%" },
            { data: 'category.name', "width": "20%" },
            {
                data: 'id',
                'render': function (data) {
                    return `<div class="w-75 btn-group" role="group">
                                <a href="Product/Upsert?id=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>Edit</a>
                                <a onClick=deleteProduct('Product/Delete?id=${data}') class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i>Delete</a>
                            </div>`
                }, /*to render multiline html elements we use ` character under the ;*/
                "width": "20%"
            } /*custom buttons*/
        ]
    });
}

function deleteProduct(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'GET',
                success: function (data) {
                    productDataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}