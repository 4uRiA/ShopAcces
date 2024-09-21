// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
//-------------------------------------------------------------------------------------
//обработчик нажатия на вкладку каталога
const ShowCatalogPage = () => {
    $('main').html(
        `<p class="TitleOfCatalog">Страница каталога</p>
        <input id="srch" data-id="search_catalog-input" placeholder="Поиск">
        <button id="srch_btn" data-id="search_catalog-btn">Поиск</button>
        <div id="catalog-grid"></div>`
    )
    SearchCatalog();
}
//Открытие страницы каталога
$(document).on('click', '.nav-link[data-page="catalog"]', function (e) {
    e.preventDefault();
    console.log('awpd')
    ShowCatalogPage();
})

//Поиск в каталоге и вывод данных
const SearchCatalog = (query) => {
    $('#catalog-grid').html('');
    $.ajax({
        url: 'api/catalog',
        method: 'GET',
        data: { query }
    }).done(function (data) {
        $.each(data, function () {
            $('#catalog-grid').append(`
        <div class="catalog_item" data-id="${this.id}">
        <img class="item_photo" src="https://parfum-asmodeus.ru/wp-content/uploads/2/c/6/2c6811fab14d1edd0ad71017eaff1240.jpeg">
        <p class="item_title">${this.name}</p>
        <p class="item_description">${this.description}</p>
        <p class="item_owner">${this.owner}</p>
        <p class="item_price"><s>8999</s>${this.price}₽</p>  
        <button class="item_btn" data-action="push_to_cart">Добавить</button>
        </div>`)
        })
    })
}

//Обработчик нажатия на кнопку поиска
$(document).on('click', 'button[data-id="search_catalog-btn"]', function () {
    let query = $('input[data-id="search_catalog-input"]').val();
    SearchCatalog(query);

})
//-------------------------------------------------------------------------------------
//Функция для добавления товара в корзину
$(document).on('click', 'button[data-action="push_to_cart"]', function () {
    let accesId = $(this).parent().attr('data-id');
    $.ajax({
        url: 'api/cart',
        method: 'POST',
        data: accesId,
        contentType: 'application/json'
    }).done(function () {
        ShowCartList();
    })
})

//Обработчик нажатия на кнопку для удаления
$(document).on('click', 'button[data-action="remove_acces"]', function () {
    let accesId = $(this).parent().attr('data-id');
    $.ajax({
        url: 'api/cart/remove',
        method: 'POST',
        data: accesId,
        contentType: 'application/json'
    }).done(function () {
        ShowCartList();
    })
})
//Функция для показа страницы корзины
const ShowCartPage = () => {
    $('main').html(
        `<p class="TitleOfCart">Корзина</p>
        <div data-id="contain_item">
        </div>
        `
    )
    ShowCartList();
}

//Функция отображения списка корзины
const ShowCartList = () => {
    
    $.ajax({
        method: 'GET',
        url: 'api/cart'
    }).done((data) => {
        let list = ''

        $.each(data, function() {
            list += 
                `
                <div data-id="${this.accessories.id}">
                <p class="item_info">${this.accessories.name} - ${this.amount}</p>
                <button class="btn_cart1" data-action="push_to_cart">Добавить</button>
                <button class="btn_cart2" data-action="remove_acces">Удалить</button>
                </div>
                `
        })
        list += `<button class="btn_cart3" data-action="do_order">Оформить заказ</button>`
        $('div[data-id="contain_item').html(list)
    })
}
//Обработчик нажатия на вкладку корзины
$(document).on('click', '.nav-link[data-page="cart"]', function (e) {
    e.preventDefault();
    console.log('awpd')
    ShowCartPage();
})

$(document).on('click', 'button[data-action="do_order"]', function () {
    $.ajax({
        method: 'POST',
        url: 'api/order',
        contentType: 'application/json'
    }).done(function () {
        ShowCartList();
    })
})

//-------------------------------------------------------------------------------------
//Обработчик для добавления нового аксессуара
$(document).on('click', '.nav-link[data-page="addAcces"]', function (e) {
    e.preventDefault();
    console.log('newAcces Done')
    ShowAddNewAccesPage();
})

//Функция для показа страницы для добавления товара
const ShowAddNewAccesPage = () => {
    $('main').html(
        `<p class="TitleOfAdd">Добавление нового аксессуара</p>
        <div class="AddArea">
        <input class="Add1" data-id="add_new_acces_name" placeholder="Название">
        <textarea class="Add2" data-id="add_new_acces_description" placeholder="Описание"></textarea>
        <input class="Add3" data-id="add_new_acces_owner" placeholder="Владалец">
        <input class="Add4" data-id="add_new_acces_price" placeholder="Цена">
        </div>
        <br>
        <div class="ForBtn_Add">
        <button class="btn_Add" data-action="add_new_acces">Добавить</button>
        </div>`
    )
}

//Функция добавления нового товара на сайт и в БД
$(document).on('click', 'button[data-action="add_new_acces"]', function () {
    let name = $('input[data-id="add_new_acces_name"]').val()
    let description = $('textarea[data-id="add_new_acces_description"]').val()
    let price = $('input[data-id="add_new_acces_price"]').val()
    let owner = $('input[data-id="add_new_acces_owner"]').val()

    $.ajax({
        method: 'POST',
        url: 'api/admin/addnewacces',
        contentType: 'application/json',
        data: JSON.stringify({
            name, 
            description,
            owner,
            price
        })
    })
})
//-------------------------------------------------------------------------------------
//Изменение списка заказов (Админ)
$(document).on('click', '.nav-link[data-page="orders"]', function (e) {
    e.preventDefault();
    console.log('editOrdersDone')
    ShowOrdersPage();
    ScreenOrders();
})

//Функция показа страницы Заказы
const ShowOrdersPage = () => {
    $('main').html(
        `
        
        <p class="TitleOfOrders">Заказы</p>
        <div data-id="contain_orders">
        </div>
        `
    )
    
}

//Функция отображения всех заказов
const ScreenOrders = () => {
    $.ajax({
        method: 'GET',
        url: 'api/order'
    }).done((data) => {
        console.log("function ScreenOrders is ready!")
        let list = ''

        $.each(data, function () {
            list +=
                    `
                    <div data-id="${this.id}">
                    <p class="TextOrders">Номер заказа: ${this.id}; Статус: ${this.status}; Пользователь: ${this.user.userName}</p>
                    `
            if (this.status == 'Создан')  
                list += `<button class="btn_orders" data-action="changes_status" data-id="${this.id}"">Изменить статус</button>`    
            
            list += `</div`
                   
                    
        })
        $('div[data-id="contain_orders').html(list)
    })
}

//Обработчик при нажатии на кнопку "Изменить статус"
$(document).on('click', 'button[data-action="changes_status"]', function () {
    orderId = $(this).attr('data-id')

    $.ajax({
        method: 'PUT',
        url: 'api/order',
        data:  orderId  ,
        contentType: 'application/json'
    }).done(() => {
        ScreenOrders();
    })
})


// Write your JavaScript code.
