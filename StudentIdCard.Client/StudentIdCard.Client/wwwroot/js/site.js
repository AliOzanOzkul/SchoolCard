// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const cartItems = [];
var productList = [];

function AddToCart(item) {
    var existingItem = cartItems.find(i => i.id === item.id);
    if (existingItem) {
        existingItem.quantity += 1;
    } else {
        item.quantity = 1;
        cartItems.push(item);
    }
    RenderCart();
}

async function FilterProduct() {
    productList.length = 0;
    var list = await GetProduct();
    $('#mainBody').empty();
  
    $.each(list, function (index,item) {
        if (item.name == 'Kapuz') {
    
            $('#mainBody').append(`
        <tr>
            <td class="mdl-data-table__cell--non-numeric"></td>
            <td class="mdl-data-table__cell--non-numeric">${item.name}</td>
            <td class="mdl-data-table__cell--non-numeric">${item.price.toFixed(2)} ₺</td>
            <td class="mdl-data-table__cell--non-numeric"><button class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect button--colored-teal" onclick='AddToCart(${JSON.stringify(item)})'>Sepete Ekle</button></td>
        </tr>
    `);
        }
    })

    $("#buttonDiv").empty();
    $('#buttonDiv').append(
        '<button class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect button--colored-teal" id="filteButton" onclick="noFilter()" >Filitre Kaldır</button>'
    )

   

}

async function noFilter() {
    productList.length = 0;
    var list = await GetProduct();
    $('#mainBody').empty();

    $.each(list, function (index, item) {
        

            $('#mainBody').append(`
        <tr>
            <td class="mdl-data-table__cell--non-numeric"></td>
            <td class="mdl-data-table__cell--non-numeric">${item.name}</td>
            <td class="mdl-data-table__cell--non-numeric">${item.price.toFixed(2)} ₺</td>
            <td class="mdl-data-table__cell--non-numeric"><button class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect button--colored-teal" onclick='AddToCart(${JSON.stringify(item)})'>Sepete Ekle</button></td>
        </tr>
    `);
        
    })

    $("#buttonDiv").empty();
    $('#buttonDiv').append(
        '<button class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect button--colored-teal" id="filteButton" onclick="FilterProduct()" >Filitrele</button>'
    )



}

function RemoveFromCart(id) {
    debugger;
    var existingItem = cartItems.find(i => i.id === id);
    if (existingItem) {
        existingItem.quantity -= 1;

        if (existingItem.quantity === 0) {
            cartItems.splice(cartItems.indexOf(existingItem), 1);
        }
    }

    RenderCart();
}

function RenderCart() {
    var totalAmount = 0;

    $('#cartBody').empty();
    $.each(cartItems, function (index, item) {
        var totalPrice = item.price * item.quantity;
        totalAmount += totalPrice;

        $('#cartBody').append(`
        <tr>
            <td class="mdl-data-table__cell--non-numeric">${index + 1}</td>
            <td class="mdl-data-table__cell--non-numeric">${item.name}</td>
            <td class="mdl-data-table__cell--non-numeric">${item.quantity}</td>
            <td class="mdl-data-table__cell--non-numeric">${totalPrice.toFixed(2)} ₺</td>
            <td class="mdl-data-table__cell--non-numeric">
            <button class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect button--colored-red" onclick='RemoveFromCart("${item.id}")'>Sepetten Çıkar</button>
            </td>
        </tr>
    `);
    });

    $('#amounth').text(totalAmount.toFixed(2) + ' ₺');

    $('#basketItems').val(JSON.stringify(cartItems));
}

function BuyItems() {
    $.ajax({
        url: '/Cantine/Index',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(cartItems),
        success: function (data) {
            alert('Ödeme başarılı');
            location.reload();
        },
        error: function (error) {
            alert('Satın alma işlemi sırasında bir hata oluştu:');
            console.log(error);
            console.log(response);
        }
    });
}

async function GetProduct() {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: '/Product/ProductList',
            type: 'Get',
            contentType: 'application/json',
         
            success: function (data) {
                var currentList = [];
                currentList = data;
                for (var i = 0; i < currentList.length; i++) {

                    productList.push(currentList[i]);
                }
                resolve(productList);

            },
            error: function (error) {
                alert('Satın alma işlemi sırasında bir hata oluştu:');
                reject(error);
            }
        });
    });
    
}

