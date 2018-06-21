$(document).ready(function () {

    //Initialize Select2 Elements
    var setSelect = function () {

        $.fn.modal.Constructor.prototype.enforceFocus = function () { };

        $('.select2').each(function () {
            $(this).select2({
                language: "ru"
            });
        });

    };

    setSelect();

    //Modal Dialog Open

      $('[data-target="#createClaim').click(function () {

        function loadClaimCreateForm(callback) {

            $('.modal-dialog').find('.preloader').show();
            $('#createClaim').find('.modal-body').load("CreateClaim form");
            setTimeout(callback, 900);
            $('.modal-dialog').find('.preloader').delay(2500).fadeOut(200);

        };

        setTimeout(function () {
            loadEmployeesList();
            loadAccountsList();
            if (!($('.modal-dialog').find('.select2').hasClass('select2-container'))) {
              setSelect();
            }
            setClaimTemplate();
        }, 3000);

        loadClaimCreateForm(setSelect);

    });


    //Modal Dialog Clear After Closing
    $('#createClaim').find('.close').click(function () {
        clearModal();
    });

    $('#createClaim').find(' form ').submit(function () {
        clearModal();
    });

    function clearModal() {
        $('#createClaim').find('.modal-body').empty();
    };


    // Mibile menu
    $('.top-menu').after('<nav id="my-menu">');
    $('.top-menu').find('ul').clone().appendTo('#my-menu');
    mMenu = $('#my-menu');
    mMenu.find('.top-menu').removeClass('hidden-xs').removeClass('hidden-sm').removeClass('top-menu');
    mMenu.find('ul').removeClass('nav').removeClass('nav-pills').removeClass('nav-stacked').removeClass('claims-tabs');
    mMenu.find('.caret').remove();
    mMenu.find('.dropdown-menu').removeClass('dropdown-menu');

    $('#my-menu').mmenu({
        extensions: ["effect-menu-slide", "pagedim-black"],
        navbar: {
            title: 'Меню'
        }
    });

    mMenu.find('.mm-next').addClass("mm-fullsubopen");

    var mmenuApi = $('#my-menu').data('mmenu');

    $('.hamburger').click(function () {
        mmenuApi.open();
    });

    $('.hamburger').click(function () {
        mmenuApi.close();
    });

    mmenuApi.bind('open:finish', function () {

        $('.hamburger').addClass('is-active');

    })
    .bind('close:before', function () {

        $('.hamburger').removeClass('is-active');

    });

    // Mobile Menu DropDown Active Link

    var activeLink = $('.dropdown-menu').find('.active-link');
    if (activeLink) {
        activeLink.closest($('.dropdown')).addClass('active-link');
    }

    //Data Tables Sort Button Disable

    hideSortingIcon();

    function hideSortingIcon() {
        var dataTable = $('.dataTable');

        dataTable.find('tr').filter(':first').children('[class*="sorting"]').each(function () {
            if ($.trim($(this).text()) == "") {
                $(this).addClass('sorting-off');
            }
        });
    };

    //Teelphone Input Mask
    $(".tel").inputmask();

    //Custom checkboxes

    $('input[type="checkbox"]').iCheck({
        checkboxClass: 'icheckbox_minimal-blue',
        radioClass: 'iradio_minimal-blue'
    });

    //Claims Data Tables Loading
    $('.claims-content').fadeIn(500);
    $('.claims-tabs').find('.tab > a').click(function () {
        $('.claims-content').css("opacity", "0");
    })
    $('.claims-content').find('.preloader').fadeOut(500);

    //Comments Block Height
    $(".direct-chat-messages").height($(".claim-details").height() - 258);
    $(".direct-chat").height($(".claim-details").height());


    //File Upload
    var wrapper = $(".file_upload"),
      inp = wrapper.find("input"),
      btn = wrapper.find(".button"),
          lbl = wrapper.find("mark");

    // Crutches for the :focus style:
    inp.focus(function () {
        wrapper.addClass("focus");
    }).blur(function () {
        wrapper.removeClass("focus");
    });

    var file_api = (window.File && window.FileReader && window.FileList && window.Blob) ? true : false;

    inp.change(function () {
        var file_name;
        if (file_api && inp[0].files[0])
            file_name = inp[0].files[0].name;
        else
            file_name = inp.val().replace("C:\\fakepath\\", '');

        if (!file_name.length)
            return;

        if (lbl.is(":visible")) {
            lbl.text(file_name);
            btn.text("Выбрать");
        } else
            btn.text(file_name);
    }).change();

    //Подгрузка лицевых счетов при выборе дома
    var loadAccountsList = function () {

      var preloader = $('.modal-dialog').find('.preloader');

        var houseList = $('#houseList');

        houseList.change(function () {


          preloader.show();

          var houseId = $(this).val();
          var baseUrl = $("#BaseUrl").data("baseurl");

          $.ajax({
            type: 'GET',
            url: baseUrl + '/PersonalAccount/GetByHouseId?houseId=' + houseId,
            success: function (data) {
              $('.personalAccountListForm').empty().append(data);
              setSelect();
              personalAccountList = $('#personalAccountList');
              loadAbonentPhone(personalAccountList);
              preloader.fadeOut(200);
            },

            error: function () {
              return false;
            }
          });

        });

    };

    //Подгрузка исполнителей при выборе вида заявки
    var loadEmployeesList = function () {

      var preloader = $('.modal-dialog').find('.preloader');

        var dictClaimKindList = $('#dictClaimKindList');

        dictClaimKindList.change(function () {

          preloader.show();

          var claimKindId = $(this).val();
          var baseUrl = $("#BaseUrl").data("baseurl");

          $.ajax({
              type: 'GET',
              url: baseUrl + '/Employees/GetByClaimKind?claimKindId=' + claimKindId,
              success: function (data) {
                $('.employeeListForm').empty().append(data);
                setSelect();
                preloader.fadeOut(200);
              },

              error: function () {
                return false;
              }
          });

          preloader.fadeOut(200);

      });

    };

    //Подгрузка телефона при выборе лицевого счета
    var loadAbonentPhone = function (list) {

      var preloader = $('.modal-dialog').find('.preloader');

      list.change(function () {

        preloader.show();

        var abonentApartmnetNumber = $(this).val();

        var baseUrl = $("#BaseUrl").data("baseurl");

        $.ajax({
            type: 'GET',
            url: baseUrl + '/PersonalAccount/GetContactPhoneNumber?personalAccountId=' + abonentApartmnetNumber,
            success: function (data) {
              $('#contactPhoneNumber').val(data);
              setSelect();
              preloader.fadeOut(200);
            },

            error: function () {
              return false;
            }
        });

        preloader.fadeOut(200);

      });

    };

    function setClaimTemplate() {

      var teplateSelect = $('.claim-template');

      teplateSelect.change(function() {

        $('.claim-description').text(teplateSelect.find("option:selected").text());
      });
    }


});
