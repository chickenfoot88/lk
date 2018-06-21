// ==AbonentScript==
// @name employeeJS
// @description функции для режима сотрудника
// @author Timur Sajfutdinov

if (!jQuery) { throw new Error("EmployeeJS requires jQuery") }


	//Непонятное назнчание фукнции
	var compItem=document.getElementsByClassName('compItem');

	if(compItem!=null){

		$(".compItem").click(function (e) {

			e.preventDefault();

			$.get(this.href, function (data) {
				$('#dialogContent').html(data);
				$('#modDialog').modal('show');
			});

		});
	};

	var storage = localStorage.getItem('item');

	if (storage && storage !== "#") {
		$('.nav-tabs a[href="' + storage + '"]').tab('show');
	}

	$('ul.nav').on('click', 'li:not(.active)', function () {
		var itemId = $(this).find('a').attr('href');
		localStorage.setItem('item', itemId);
	});

	var link = window.location.pathname;
	$('nav li.active').removeClass('active-link');
	var variable = false;
	$('nav li a').each(function () {
		if ($(this).attr('href').indexOf(link) == 0 && !variable) {
			$(this).parent('li').addClass('active-link');
			variable = true;
		}
	});

	var url = '/Employee/CountNewMessage';
	$.get(url, function (response) {
		if (response > 0)
		$('#CountNewMessage').append(response);
	});

	url = '/Employee/CountNewPoll';
	$.get(url, function (response) {
		if (response > 0)
		$('#CountNewPoll').append(response);
	});

	$("#dataImportDataTable").DataTable({
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
      "emptyTable": "Таблица пустая",
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

		$("#dataExportDataTable").DataTable({
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
				"emptyTable": "Таблица пустая",
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

			$('#abonentClaimsDataTable').DataTable({
      	"paging": true,
        "lengthChange": false,
        "searching": true,
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

			$("#countersdom").DataTable({
				"paging": true,
        "lengthChange": false,
        "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "Все"]],
        "searching": false,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "language": {
          "lengthMenu": "Показано _MENU_ записей на странице",
          "zeroRecords": "Ничего не найдено",
          "info": "Показаны с _START_ по _END_ из _TOTAL_ записей",
          "infoEmpty": "Записи отсутствуют",
          "infoFiltered": "(отфильтровано от общего количества записей _MAX_)",
          "emptyTable": "Таблица пустая",
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

			$("#countersls").DataTable({
				"paging": true,
				"lengthChange": false,
				//"lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "Все"]],
				"searching": false,
				"ordering": true,
				"info": true,
				"autoWidth": false,
				"language": {
					"lengthMenu": "Показано _MENU_ записей на странице",
					"zeroRecords": "Ничего не найдено",
					"info": "Показаны с _START_ по _END_ из _TOTAL_ записей",
					"infoEmpty": "Записи отсутствуют",
					"infoFiltered": "(отфильтровано от общего количества записей _MAX_)",
					"emptyTable": "Таблица пустая",
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

			$("#countersord").DataTable({
				"paging": true,
				"lengthChange": true,
				"lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "Все"]],
				"searching": false,
				"ordering": true,
				"info": true,
				"autoWidth": false,
				"language": {
					"lengthMenu": "Показано _MENU_ записей на странице",
					"zeroRecords": "Ничего не найдено",
					"info": "Показаны с _START_ по _END_ из _TOTAL_ записей",
					"infoEmpty": "Записи отсутствуют",
					"infoFiltered": "(отфильтровано от общего количества записей _MAX_)",
					"emptyTable": "Таблица пустая",
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


			$('#example2').DataTable({
	      "paging": true,
	      "lengthChange": false,
	      "searching": true,
	      "ordering": true,
	      "info": true,
	      "autoWidth": false
	  	});

	  	$('#example3').DataTable({
				"paging": true,
	      "lengthChange": false,
	      "searching": false,
	      "ordering": true,
	      "info": true,
	      "autoWidth": false
		  });

			$('#example4').DataTable({
	      "paging": true,
	      "lengthChange": false,
	      "searching": true,
	      "ordering": true,
	      "info": true,
	      "autoWidth": false
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
	                url: '/Abonent/SaveCounters',
	                type: 'GET',
	                data: { enteredValue: ValCnt[i].value, meterReadingId: MeterReadingId[i].value },
	                success: window.location.href = '/Employee/Deposition'
	            })
	          }

					});
				}

				var EnterValCnt=document.getElementById('EnterDomValCnt');
				if(EnterValCnt!=null){
					EnterValCnt.addEventListener('click', function () {
						var elems = document.getElementsByName("InputDomValCnt");
						for (var i = 0; i < elems.length; i++)
						elems[i].readOnly = false;
						document.getElementById("EnterDomValCnt").disabled = true;
						document.getElementById("SaveDomValCnt").disabled = false;
					});
				}

				var SaveValCnt=document.getElementById('SaveDomValCnt');
				if(SaveValCnt!=null){
					SaveValCnt.addEventListener('click', function () {
						var ValCnt = document.getElementsByName("InputDomValCnt");
						var MeterReadingId = document.getElementsByName("MeterReadingId");
						for (var i = 0; i < ValCnt.length; i++)
						ValCnt[i].readOnly = true;
						document.getElementById("EnterDomValCnt").disabled = false;
						document.getElementById("SaveDomValCnt").disabled = true;

						for (var i = 0; i < ValCnt.length; i++) {
							$.ajax({
								url: '/Employee/SaveCounters',
								type: 'GET',
								data: { enteredValue: ValCnt[i].value, meterReadingId: MeterReadingId[i].value },
								success: window.location.href = '/Employee/Deposition'
							})
						}
					});
				}
