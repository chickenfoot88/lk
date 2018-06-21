// ==DirectorScript==
// @name directorJS
// @description функции для режима руководителя
// @author Adel Ismagilov

	$(document).ready(function() {

		$('#directorPaymentsDataTable').DataTable({
			"paging": false,
			"lengthChange": false,
			"searching": false,
			"ordering": false,
			"info": false,
			"autoWidth": false,
		});

// Тестовые данные для графиков
// Начисления и оплата
var chartDataPayments = [ [104, 157, 220, 305, 391, 350], [95, 120, 140, 20, 90, 332] ];
// Собираемость
var chartDataCollections = [10, 15, 19, 10, 10, 13];
// Приборы учета
// Инвидиуальные
var chartDataServicesIndividual = [23, 43, 12, 32, 43, 32];
// Общедомовые
var chartDataServicesCommon = [5, 30, 33, 76, 21, 2];
// Заявки
// По типу
var chartDataClaimsType = [ [10, 20, 15, 13, 7, 3], [3, 14, 12, 2, 1, 2], [7, 6, 3, 11, 6, 1] ];
var chartDataClaimsPeriod = [ [20, 34, 38, 26, 14, 6]];

//Конструктор фукции рендера графиков
	function chartMaker(chartData, chartType, stack) {

		var chartColors = [
			'rgb(79, 152, 195)',
			'rgb(0, 166, 90)',
			'rgb(243, 156, 18)'
		];

	// Проряем данные передаваемы в фукнцию и формируем datasets для графиков
		var datasets = [];

		for (var i = 0; i < (Array.isArray(chartData[i]) ? chartData.length : 1); i++) {
			datasets.push ({
				label: arguments[i+3],
				data: Array.isArray(chartData[i]) ? chartData[i] : chartData,
				backgroundColor: chartType === 'bar' ? chartColors[i] : 'rgba(0, 0, 0, 0)',
				borderColor: chartType === 'bar' ? 'rgba(0, 0, 0, 0)' : chartColors[i],
				lineTension: 0
			})
		}

		this.renderChart = function(canvasClassName) {

			var myChart = new Chart(canvasClassName, {

	      type: chartType,

	      data: {
	        labels: ["Январь", "Февраль", "Март", "Апрель", "Май", "Июнь"],
	        datasets: datasets
	      },

	      options: {
	        scales: {
	          yAxes: [{
							stacked: stack === 'stacked' ? true : false,
	            ticks: {
              	beginAtZero:true,
			        }
	          }],
	          xAxes: [{
							stacked: stack === 'stacked' ? true : false,
	            maxBarThickness: 60,
	          }]
	        },
					legend: {
						display: !!datasets[0].label ? true: false,
					},
					tooltips: {
						titleMarginBottom: 15
					}
				},
	  	});

		}

	};

	chartMaker.prototype = {
		makeChart: function(canvasClassName) {

			if (canvasClassName.length === 0) {
				return false;
			}

			return this.renderChart(canvasClassName);
		}
	};

	var chartPayments = new chartMaker(chartDataPayments, 'bar', 'noStacked', 'Начислено', 'Оплачено');
	var chartCollections = new chartMaker(chartDataCollections, 'bar', 'noStacked', 'Собираемость, %');
	var chartServicesIndividual = new chartMaker(chartDataServicesIndividual, 'line', 'noStacked', '');
	var chartServicesCommon = new chartMaker(chartDataServicesCommon, 'line', 'noStacked', '');
	var chartServicesCommon = new chartMaker(chartDataServicesCommon, 'line', 'noStacked', '');
	var chartClaimsType = new chartMaker(chartDataClaimsType, 'bar', 'stacked', 'Открыте', 'Закрытые', 'В работе');
	var chartClaimsPeriod = new chartMaker(chartDataClaimsPeriod, 'bar', 'noStacked', '');

	chartPayments.makeChart($('.chart-payments'));
	chartCollections.makeChart($('.chart-collections'));
	chartServicesIndividual.makeChart($('.chart-services-individual'));
	chartServicesCommon.makeChart($('.chart-services-common'));
	chartClaimsType.makeChart($('.chart-claims-type'));
	chartClaimsPeriod.makeChart($('.chart-claims-period'));

// График заявок по типам
	function renderChartClaimsType(data1, data2, data3, color1, color2, color3) {

			var ctx = $('.bar-chart-claims-type');

			var myChart = new Chart(ctx, {

				type: 'bar',
				data: {
					labels: ["Уборка подъезда", "Содержание двора", "Водоснабжение", "Отопление", "Прочее"],
					datasets: [{
						label: 'Открытые',
						data: data1,
						backgroundColor: color1
					}, {
						label: 'Закрытые',
						data: data2,
						backgroundColor: color2
					}, {
						label: 'В работе',
						data: data3,
						backgroundColor: color3
					}]
				},
				options: {
					scales: {
						yAxes: [{
							stacked: true,
							ticks: {
									beginAtZero:true,
							}
						}],
						xAxes: [{
							stacked: true,
							maxBarThickness: 60
						}],
					},
				}

			});
		}
});
