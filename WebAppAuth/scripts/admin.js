$("#usersList").DataTable({
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
