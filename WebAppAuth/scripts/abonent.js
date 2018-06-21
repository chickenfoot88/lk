// ==AbonentScript==
// @name abonentJS
// @description функции для режима абонента
// @author Timur Sajfutdinov

if (!jQuery) { throw new Error("AbonentScript requires jQuery") }

        var baseUrl =  $("#BaseUrl").data("baseurl");

	var SelPool=document.getElementById('SelPool');
	if(SelPool!=null){
            SelPool.OnClick(function (e) {
                e.preventDefault();
                $.get(this.href, function (data) {
                    $('#dialogContent').html(data);
                    $('#modDialog').modal('show');
                });
            });
	}

        function XFormatPrice(_number, rub) {
            var decimal = 2;
            var separator = ' ';
            var decpoint = '.';
            if (rub)
                var format_string = '# руб.';
            else
                var format_string = '#';
            var r = parseFloat(_number);
            var exp10 = Math.pow(10, decimal);// приводим к правильному множителю
            r = Math.round(r * exp10) / exp10;// округляем до необходимого числа знаков после запятой

            rr = Number(r).toFixed(decimal).toString().split('.');

            b = rr[0].replace(/(\d{1,3}(?=(\d{3})+(?:\.\d|\b)))/g, "\$1" + separator);

            r = (rr[1] ? b + decpoint + rr[1] : b);
            return format_string.replace('#', r);
        }
//        $(document).ready(function (e) {
            var link = window.location.pathname;
            $('nav li.active').removeClass('active-link');
            var variable = false;
            $('nav li a').each(function () {
                if ($(this).attr('href').indexOf(link) == 0 && !variable) {
                    $(this).parent('li').addClass('active-link');
                    variable = true;
                }
            });

  /*          var url = baseUrl + '/Abonent/GetPersonalAccAddress';
            $.get( url, function (response) {
                if (response != null)
                    $('#PersonalAccAddress').append(response);
            });

            var url = baseUrl + '/Abonent/CountNewMessage';
            $.get(url, function (response) {
                if (response > 0)
                    $('#CountNewMessage').append(response);
            });

            var url = baseUrl + '/Abonent/CountNewPoll';
            $.get(url, function (response) {
                if (response > 0)
                    $('#CountNewPoll').append(response);
            });
 */

//       });

            $("#personalAccountsDataTable").DataTable({
                "paging": true,
                "pageLength": 10,
                "lengthChange": false,
                "searching": false,
                "ordering": true,
                "info": false,
                "autoWidth": false,
                "language": {
                    "lengthMenu": "Показано _MENU_ записей на странице",
                    "zeroRecords": "Ничего не найдено",
                    "info": "Показаны с _START_ по _END_ из _TOTAL_ записей",
                    "infoEmpty": "Записи отсутствуют",
                    "infoFiltered": "(отфильтровано от общего количества записей _MAX_)",
                    "emptyTable": "Данные отсутствуют в таблице",
                    "loadingRecords": "Загрузка...",
                    "processing": "Обработка...",
                    "search": "Поиск:",
                    "paginate": {
                        "first": "<<",
                        "last": ">>",
                        "next": ">",
                        "previous": "<"
                    }
                }
            });

            $('#claimsDataTable').DataTable({
                "paging": true,
                "lengthChange": false,
                "searching": false,
                "ordering": true,
                "info": true,
                "autoWidth": false,
                "language": {
                    "paginate": {
                        "first": "<<",
                        "last": ">>",
                        "next": ">",
                        "previous": "<"
                    }
                }
            });

            $('#examplecharge').DataTable({
                "paging": false,
                "pageLength": 50,
                "lengthChange": false,
                "lengthMenu": [[10, 25, 50], [10, 25, 50]],
                "searching": false,
                "ordering": false,
                "info": false,
                "autoWidth": false,
                "language": {
                    "lengthMenu": "Показано _MENU_ записей на странице",
                    "zeroRecords": "Ничего не найдено",
                    "info": "Показаны с _START_ по _END_ из _TOTAL_ записей",
                    "infoEmpty": "Записи отсутствуют",
                    "infoFiltered": "(отфильтровано от общего количества записей _MAX_)",
                    "emptyTable": "Данные отсутствуют в таблице",
                    "loadingRecords": "Загрузка...",
                    "processing": "Обработка...",
                    "search": "Поиск:",
                    "paginate": {
                        "first": "<<",
                        "last": ">>",
                        "next": ">",
                        "previous": "<"
                    },
                },
                "footerCallback": function (row, data, start, end, display) {
                    var api = this.api(), data;

                    if ($("tfoot").length)
                        document.getElementById("examplecharge").deleteTFoot();
                    // Remove the formatting to get integer data for summation
                    var intVal = function (i) {

                        return typeof i === 'string' ?
                            +((i.replace(/(<\/?[^>]+>)/gi, '')).replace(' ', '')).replace('&nbsp;', '') :
                            typeof i === 'number' ?
                            i : 0;
                    };


                    // Total over all pages
                    summoney = api
                        .column(4)
                        .data()
                        .reduce(function (a, b) {
                            return intVal(a) + intVal(b);
                        }, 0);
                    suminsaldo = api
                        .column(5)
                        .data()
                        .reduce(function (a, b) {
                            return intVal(a) + intVal(b);
                        }, 0);
                    sumtarif = api
                        .column(6)
                        .data()
                        .reduce(function (a, b) {
                            return intVal(a) + intVal(b);
                        }, 0);
                    reval = api
                        .column(7)
                        .data()
                        .reduce(function (a, b) {
                            return intVal(a) + intVal(b);
                        }, 0);

                    sumnedop = api
                        .column(8)
                        .data()
                        .reduce(function (a, b) {
                            return intVal(a) + intVal(b);
                        }, 0);
                    sumoutsaldo = api
                        .column(9)
                        .data()
                        .reduce(function (a, b) {
                            return intVal(a) + intVal(b);
                        }, 0);
                    sumcharge = api
                        .column(10)
                        .data()
                        .reduce(function (a, b) {
                            return intVal(a) + intVal(b);
                        }, 0);

                    var tfoot = '<tfoot class="i"><tr>';
                    tfoot += '<th colspan="4" style="text-align: right">Итого:</th>';
                    tfoot += '<th colspan="1" style="text-align: right">' + XFormatPrice(summoney, false) + '</th>';
                    tfoot += '<th colspan="1" style="text-align: right">' + XFormatPrice(suminsaldo, false) + '</th>';
                    tfoot += '<th colspan="1" style="text-align: right">' + XFormatPrice(sumtarif, false) + '</th>';
                    tfoot += '<th colspan="1" style="text-align: right">' + XFormatPrice(reval, false) + '</th>';
                    tfoot += '<th colspan="1" style="text-align: right">' + XFormatPrice(sumnedop, false) + '</th>';
                    tfoot += '<th colspan="1" style="text-align: right">' + XFormatPrice(sumoutsaldo, false) + '</th>';
                    tfoot += '<th colspan="1" style="text-align: right">' + XFormatPrice(sumcharge, false) + '</th>';
                    tfoot += '</tr></tfoot>';
                    $('#examplecharge').append(tfoot);

                }
            });

            $('#examplesocialpayment').DataTable({
                "paging": true,
                "pageLength": 25,
                "lengthChange": false,
                "lengthMenu": [[10, 25, 50], [10, 25, 50]],
                "searching": false,
                "ordering": false,
                "info": false,
                "autoWidth": false,
                "language": {
                    "lengthMenu": "Показано _MENU_ записей на странице",
                    "zeroRecords": "Ничего не найдено",
                    "info": "Показаны с _START_ по _END_ из _TOTAL_ записей",
                    "infoEmpty": "Записи отсутствуют",
                    "infoFiltered": "(отфильтровано от общего количества записей _MAX_)",
                    "emptyTable": "Данные отсутствуют в таблице",
                    "loadingRecords": "Загрузка...",
                    "processing": "Обработка...",
                    "search": "Поиск:",
                    "paginate": {
                        "first": "<<",
                        "last": ">>",
                        "next": ">",
                        "previous": "<"
                    },
                },
                "footerCallback": function (row, data, start, end, display) {
                    var api = this.api(), data;

                    if ($("tfoot").length)
                        document.getElementById("examplesocialpayment").deleteTFoot();
                    // Remove the formatting to get integer data for summation
                    var intVal = function (i) {

                        return typeof i === 'string' ?
                            +((i.replace(/(<\/?[^>]+>)/gi, '')).replace(' ', '')).replace('&nbsp;', '') :
                            typeof i === 'number' ?
                            i : 0;
                    };


                    // Total over all pages
                    var sumPayoff = api
                        .column(5)
                        .data()
                        .reduce(function (a, b) {
                            return intVal(a) + intVal(b);
                        }, 0);

                    var tfoot = '<tfoot class="i"><tr>';
                    tfoot += '<th colspan="5" style="text-align: right">Итого:</th>';
                    tfoot += '<th colspan="1" style="text-align: right">' + XFormatPrice(sumPayoff, false) + '</th>';
                    tfoot += '</tr></tfoot>';
                    $('#examplesocialpayment').append(tfoot);

                }
            });

 	$("#examplepayment").DataTable({
                "paging": true,
                "lengthChange": false,
                "searching": false,
                "ordering": false,
                "info": false,
                "autoWidth": false,
                "language": {
                    "lengthMenu": "Показано _MENU_ записей на странице",
                    "zeroRecords": "Ничего не найдено",
                    "info": "Показаны с _START_ по _END_ из _TOTAL_ записей",
                    "infoEmpty": "Записи отсутствуют",
                    "infoFiltered": "(отфильтровано от общего количества записей _MAX_)",
                    "emptyTable": "Данные отсутствуют в таблице",
                    "loadingRecords": "Загрузка...",
                    "processing": "Обработка...",
                    "search": "Поиск:",
                    "paginate": {
                        "first": "<<",
                        "last": ">>",
                        "next": ">",
                        "previous": "<"
                    }
                },
                "footerCallback": function (row, data, start, end, display) {
                    var api = this.api(), data;

                    if ($("tfoot").length)
                        document.getElementById("examplepayment").deleteTFoot();
                    // Remove the formatting to get integer data for summation
                    var intVal = function (i) {

                        return typeof i === 'string' ?
                            +((i.replace(/(<\/?[^>]+>)/gi, '')).replace(' ', '')).replace('&nbsp;', '') :
                            typeof i === 'number' ?
                            i : 0;
                    };


                    // Total over all pages
                    summoney = api
                        .column(5)
                        .data()
                        .reduce(function (a, b) {
                            return intVal(a) + intVal(b);
                        }, 0);

                    var tfoot = '<tfoot class="i"><tr>';
                    tfoot += '<th colspan="5" style="text-align: right">Итого:</th>';
                    tfoot += '<th colspan="1" style="text-align: right">' + XFormatPrice(summoney, false) + '</th>';
                    tfoot += '</tr></tfoot>';
                    $('#examplepayment').append(tfoot);

                }
            });

            //iCheck for checkbox and radio inputs
            $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
                checkboxClass: 'icheckbox_minimal-blue',
                radioClass: 'iradio_minimal-blue'
            });
            //Red color scheme for iCheck
            $('input[type="checkbox"].minimal-red, input[type="radio"].minimal-red').iCheck({
                checkboxClass: 'icheckbox_minimal-red',
                radioClass: 'iradio_minimal-red'
            });
            //Flat red color scheme for iCheck
            $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green'
            });
            //Enable iCheck plugin for checkboxes
            //iCheck for checkbox and radio inputs
            $('.mailbox-messages input[type="checkbox"]').iCheck({
                checkboxClass: 'icheckbox_flat-blue',
                radioClass: 'iradio_flat-blue'
            });

            //Enable check and uncheck all functionality
            $(".checkbox-toggle").click(function () {
                var clicks = $(this).data('clicks');
                if (clicks) {
                    //Uncheck all checkboxes
                    $(".mailbox-messages input[type='checkbox']").iCheck("uncheck");
                    $(".fa", this).removeClass("fa-check-square-o").addClass('fa-square-o');
                } else {
                    //Check all checkboxes
                    $(".mailbox-messages input[type='checkbox']").iCheck("check");
                    $(".fa", this).removeClass("fa-square-o").addClass('fa-check-square-o');
                }
                $(this).data("clicks", !clicks);
            });

            //Handle starring for glyphicon and font awesome
            $(".mailbox-star").click(function (e) {
                e.preventDefault();
                //detect type
                var $this = $(this).find("a > i");
                var glyph = $this.hasClass("glyphicon");
                var fa = $this.hasClass("fa");

                //Switch states
                if (glyph) {
                    $this.toggleClass("glyphicon-star");
                    $this.toggleClass("glyphicon-star-empty");
                }

                if (fa) {
                    $this.toggleClass("fa-star");
                    $this.toggleClass("fa-star-o");
                }
            });

 	    function AddClaim(e) {
                $.ajax({
                    url:  baseUrl + '/Abonent/CreateClaim',
                    type: 'GET'
                });
            };

            function EditClaim(e) {
                var Id = document.getElementById("SelId").value;
                $.ajax({
                    url:  baseUrl + '/Abonent/DetailClaims',
                    type: 'GET',
                    data: { Id: Id },
                    success: function (data) {
                        $('#dialogContent').html(data);
                        $('#modDialog').modal('show');
                    }

                });
            };

            function DeleteClaim(e) {
                var Id = document.getElementById("SelId").value;
                $.ajax({
                    url:  baseUrl + '/Abonent/DeleteClaim',
                    type: 'POST',
                    data: { rid: Id },
                    success: function () { window.location.href =  baseUrl + '/Abonent/Claims'; }
                }

                );
            };

            function sendForm(e) {
                var x = document.getElementById("ClaimTypes").selectedIndex;
                var y = document.getElementById("ClaimTypes").options;
                var rtext = document.getElementById("rtext").value;
                if (e == 2) {
                    var rid = document.getElementById("rid").value;
                    $.ajax({
                        url:  baseUrl + '/Abonent/UpdateClaim',
                        type: 'POST',
                        data: { rtype: y[x].text, rtext: rtext, rid: rid },
                        success: function (data) {
                            $('.modal').modal('hide');
                            window.location.href =  baseUrl + '/Abonent/Claims';
                        }
                    });
                };
                if (e == 1) {
                    $.ajax({
                        url:  baseUrl + '/Abonent/InsertClaim',
                        type: 'POST',
                        data: { rtype: y[x].text, rtext: rtext },
                        success: function (data) {
                            $('.modal').modal('hide');
                            window.location.href =  baseUrl + '/Abonent/Claims';
                        }
                    });
                };
                return false;
            };

 	    window.addEventListener('load', function () {
              var CalculationDateSelectList=document.getElementById('CalculationDateSelectList');
              if(CalculationDateSelectList!=null){


                if (CalculationDateSelectList.options.selectedIndex == 0) {
                    document.getElementById("EnterValCnt").disabled = false;
                }
                else {
                    document.getElementById("EnterValCnt").disabled = true;
                }
	      }
            });
            var EnterValCnt=document.getElementById('EnterValCnt');
            if(EnterValCnt!=null){
             EnterValCnt.addEventListener('click', function () {
                var elems = document.getElementsByName("InputValCnt");
                for (var i = 0; i < elems.length; i++)
                    elems[i].readOnly = false;
                document.getElementById("EnterValCnt").disabled = true;
                document.getElementById("SaveValCnt").disabled = false;
             });
            }

            var SaveValCnt=document.getElementById('SaveValCnt');
            if(SaveValCnt!=null){
                SaveValCnt.addEventListener('click', function () {
                var ValCnt = document.getElementsByName("InputValCnt");
                var MeterReadingId = document.getElementsByName("MeterReadingId");
                for (var i = 0; i < ValCnt.length; i++)
                    ValCnt[i].readOnly = true;
                document.getElementById("EnterValCnt").disabled = false;
                document.getElementById("SaveValCnt").disabled = true;

                for (var i = 0; i < ValCnt.length; i++) {
                    $.ajax({
                        url:  baseUrl + '/Abonent/SaveCounters',
                        type: 'GET',
                        data: { enteredValue: ValCnt[i].value, meterReadingId: MeterReadingId[i].value },
                        success: window.location.href =  baseUrl + '/Abonent/Counters'
                    });
                }
              });
            }

 	    function CountNewPool(e) {
                    var Sel = $this.hasElement("SelId");
                    if (Sel) then;
	         {
                        var Id = document.getElementById("SelId").value;
                        $.ajax({
                            url:  baseUrl + '/Abonent/DeletePool',
                            type: 'POST',
                            data: { rid: Id },
                            success: function () { window.location.href =  baseUrl + '/Abonent/Interviews'; }
                        });
	         }
                };

	    var SetVote=document.getElementById('SetVote');
            if(SetVote!=null){

             SetVote.addEventListener('click', function () {
                var Id = document.getElementById("SelId").value;
                var inp = document.getElementsByName('radiobuttonvote');
                for (var i = 0; i < inp.length; i++) {
                    if (inp[i].type == "radio" && inp[i].checked) {
                        var poolvariantid = inp[i].value;
                    }
                }
                $.ajax({
                    url:  baseUrl + '/Abonent/SetVote',
                    type: 'POST',
                    data: { PoolId: Id, PoolVarianId: poolvariantid },
                    success: function (data) {
                        window.location.reload();
                    }
                });
             });
	    }


            function AddPool(e) {
                $.ajax({
                    url:  baseUrl + '/Abonent/CreatePool',
                    type: 'GET',
                    success: function (data) {
                        $('#dialogContent').html(data);
                        $('#modDialog').modal('show');
                    }

                });
            };

            function EditPool(e) {
                var Id = document.getElementById("SelId").value;
                $.ajax({
                    url:  baseUrl + '/Abonent/DetailPool',
                    type: 'GET',
                    data: { Id: Id },
                    success: function (data) {
                        $('#dialogContent').html(data);
                        $('#modDialog').modal('show');
                    }

                });
            };

            function DeletePool(e) {
                var Id = document.getElementById("SelId").value;
                $.ajax({
                    url:  baseUrl + '/Abonent/DeletePool',
                    type: 'POST',
                    data: { rid: Id },
                    success: function () { window.location.href =  baseUrl + '/Abonent/Interviews'; }
                }

                );
            };

            function inline(e) {
                if (e == true) {
                    var inp = document.getElementsByName('rval');
                    for (var i = 0; i < inp.length; i++) {
                        if (inp[i].style.display == "none") {
                            inp[i].style.display = "inline-block";
                            break;
                        }
                    }

                }
                else {
                    var inp = document.getElementsByName('rval');
                    for (var i = inp.length - 1; i > 0; i--) {
                        if (inp[i].style.display == "inline-block") {
                            inp[i].style.display = "none";
                            inp[i].value = '';
                            break;
                        }
                    }
                }
            };
            function CreateOrEditPool(e) {
                var rname = document.getElementById("rname").value;
                var rcomment = document.getElementById("rcomment").value;
                var rdate = document.getElementById("rdate").value;
                if (e == 2) {
                    var rid = document.getElementById("rid").value;
                    $.ajax({
                        url:  baseUrl + '/Abonent/UpdatePool',
                        type: 'POST',
                        data: { rname: rname, rcomment: rcomment, rdate: rdate, rid: rid },
                        success: function (data) {
                            $('.modal').modal('hide');
                            window.location.href =  baseUrl + '/Abonent/Interviews';
                        }
                    });
                };
                if (e == 1) {
                    var arr = new Array();
                    var inp = document.getElementsByName('rval');
                    for (var i = 0; i < inp.length; i++) {
                        if (inp[i].style.display == "inline-block") {
                            arr.push(inp[i].value);
                        }
                    }
                    $.ajax({
                        url:  baseUrl + '/Abonent/InsertPool',
                        type: 'POST',
                        data: { rname: rname, rcomment: rcomment, rdate: rdate, arr: arr },
                        success: function (data) {
                            $('.modal').modal('hide');
                            window.location.href =  baseUrl + '/Abonent/Interviews';
                        }
                    });
                };
                return false;
            };
