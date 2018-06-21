// ==ReportScript==
// @name ReportJS
// @description функции для режима отчеты
// @author Timur Sajfutdinov

if (!jQuery) { throw new Error("ReportJS requires jQuery") }

 $(function(){
  // $('#reservation').daterangepicker();
  $('#reservation').daterangepicker({
    "locale": {
      applyLabel: 'Ок',
      cancelLabel: 'Отмена',
      fromLabel: 'С',
      toLabel: 'По',
      monthNames: ['Яварь', 'Февраль', 'Март', 'Апрель',
          'Май', 'Июнь', 'Июль', 'Август', 'Сентябрь',
          'Октябрь', 'Ноябрь', 'Декабрь'],
      daysOfWeek: ['Вс', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб'],
      firstDay: 1
    }
  });
});
