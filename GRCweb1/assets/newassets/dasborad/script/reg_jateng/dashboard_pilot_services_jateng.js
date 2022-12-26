var Index = function () {
    // Start : Pilot Vessel Progress
    var pilotvesselprogEmas = function () {
        $.ajax({
            url: "/PilotVesselProgress/vesselprogEmas",
            type: "GET",
            contentType: "application/json",
            dataType: "json",
            success: function (data) {
                DrawVesselProgressEmas(data);
            }
        });
    }

    var pilotvesselprogIntan = function () {
        $.ajax({
            url: "/PilotVesselProgress/vesselprogIntan",
            type: "GET",
            contentType: "application/json",
            dataType: "json",
            success: function (data) {
                DrawVesselProgressIntan(data);
            }
        });
    }

    var DrawVesselProgressEmas = function (data) {
        $("#blmEmas").text(data[0].BELUM_PENETAPAN);
        $("#approveEmas").text(data[0].APPROVE);
        $("#spkEmas").text(data[0].SPK);
        $("#ohnEmas").text(data[0].OHN);
        $("#pobEmas").text(data[0].POB);
    }

    var DrawVesselProgressIntan = function (data) {
        $("#blmIntan").text(data[0].BELUM_PENETAPAN);
        $("#approveIntan").text(data[0].APPROVE);
        $("#spkIntan").text(data[0].SPK);
        $("#ohnIntan").text(data[0].OHN);
        $("#pobIntan").text(data[0].POB);
    }
    // End : Pilot Vessel Progress




    // Start : Pilot of the Month
    var pilotpfthemonthEmas = function () {
        $.ajax({
            url: "/PilotVesselProgress/pilotofweekitemEmas",
            type: "GET",
            contentType: "application/json",
            dataType: "json",
            success: function (data) {
                DrawPanduWeekEmas(data);
            }
        });
    }

    var pilotpfthemonthIntan = function () {
        $.ajax({
            url: "/PilotVesselProgress/pilotofweekitemIntan",
            type: "GET",
            contentType: "application/json",
            dataType: "json",
            success: function (data) {
                DrawPanduWeekIntan(data);
            }
        });
    }

    var DrawPanduWeekEmas = function (data) {
        $('#namepEmas').html(data.NAMA_PETUGAS);
        $('#nippEmas').html(data.NIP_PANDU);
        $('#utilEmas').html(data.UTILITAS);
        $('#gerakanEmas').html(data.GERAKAN);
    }

    var DrawPanduWeekIntan = function (data) {
        $('#namepIntan').html(data.NAMA_PETUGAS);
        $('#nippIntan').html(data.NIP_PANDU);
        $('#utilIntan').html(data.UTILITAS);
        $('#gerakanIntan').html(data.GERAKAN);
    }
    // End : Pilot of the Month


    // Start : WTP
    var RequestWTPEmas = function () {
        $.ajax({
            url: "/PilotVesselProgress/wtpservicesEmas",
            type: "GET",
            contentType: "application/json",
            dataType: "json",
            success: function (data) {
                DrawWTPEmas(data);
            }
        });
    }

    var RequestWTPIntan = function () {
        $.ajax({
            url: "/PilotVesselProgress/wtpservicesIntan",
            type: "GET",
            contentType: "application/json",
            dataType: "json",
            success: function (data) {
                DrawWTPIntan(data);
            }
        });
    }

    var DrawWTPEmas = function (data) {
        var excellent = data.EXCELLENT;
        var fair = data.FAIR;
        var poor = data.POOR;
        var wtp = data.WTP;
        if (wtp < excellent) {
            $('#statusEmas').html("<span class='badge badge-success'>EXCELLENT</span>");
        } else if (wtp > excellent && wtp < poor) {
            $('#statusEmas').html("<span class='badge badge-primary'>FAIR</span>");
        } else if (wtp > fair ) {
            $('#statusEmas').html("<span class='badge badge-danger'>POOR</span>");
        }
        $('#wtpEmas').html(data.WTP + "<span class='h3 fw-200 text-muted'> minutes</span>");
    }

    var DrawWTPIntan = function (data) {
        var excellent = data.EXCELLENT;
        var fair = data.FAIR;
        var poor = data.POOR;
        var wtp = data.WTP;
        if (wtp < excellent) {
            $('#statusIntan').html("<span class='badge badge-success'>EXCELLENT</span>");
        } else if (wtp > excellent && wtp < poor) {
            $('#statusIntan').html("<span class='badge badge-primary'>FAIR</span>");
        } else if (wtp > fair) {
            $('#statusIntan').html("<span class='badge badge-danger'>POOR</span>");
        }
        $('#wtpIntan').html(data.WTP + "<span class='h3 fw-200 text-muted'> minutes</span>");
    }
    // End : WTP


    var vp_reqtoday = function () {
        $('#blmEmas').click(function () {
            $("#m_modal_vp").modal({
                keyboard: false,
                backdrop: "static"
            });
            $("#m_modal_vp_title").text("Vessel Progress : Request Today | Tanjung Emas");

            $("#table_detail_vp").DataTable({
                "ajax": {
                    "url": "/PilotVesselProgress/getDataTableReqTodayEmas",
                    "type": "GET",
                    "data": function (data) {
                        
                    }
                },
                "columns": [
                    {
                        "name": "NO",
                        "title": "NO",
                        "data": null,
                        "render": function (data, type, full, meta) {
                            return meta.row + meta.settings._iDisplayStart + 1;
                        }
                    },
                    {
                        "name": "NO_PPK1",
                        "title": "NO PPK1",
                        "data": "NO_PPK1"
                    },
                    {
                        "name": "NO_PPK_JASA",
                        "title": "NO PPK JASA",
                        "data": "NO_PPK_JASA"
                    },
                    {
                        "name": "NAMA_KAPAL",
                        "title": "NAMA KAPAL",
                        "data": "NAMA_KAPAL"
                    },
                    {
                        "name": "NAMA_AGEN",
                        "title": "NAMA_AGEN",
                        "data": "NAMA_AGEN"
                    },
                    {
                        "name": "GT_LOA",
                        "title": "GT - LOA",
                        "data": "GT_LOA"
                    },
                    {
                        "name": 'TGL_PERMOHONAN',
                        "data": 'TGL_PERMOHONAN',
                        "title": 'TGL PERMOHONAN',
                        "searchable": true,
                        "sortable": true,
                        "render": function (data) {
                            var re = /-?\d+/;
                            var m = re.exec(data);
                            var d = new Date(parseInt(m[0]));
                            moment.locale('pt');
                            var M = moment(d).format('DD-MM-YYYY (HH:mm)');
                            return M;
                        }
                    },
                    {
                        "name": "DARI",
                        "title": "DARI",
                        "data": "DARI"
                    },
                    {
                        "name": "KE",
                        "title": "KE",
                        "data": "KE"
                    },
                    {
                        "name": 'CREATED_DATE',
                        "data": 'CREATED_DATE',
                        "title": 'CREATED DATE',
                        "searchable": true,
                        "sortable": true,
                        "render": function (data) {
                            var re = /-?\d+/;
                            var m = re.exec(data);
                            var d = new Date(parseInt(m[0]));
                            moment.locale('pt');
                            var M = moment(d).format('DD-MM-YYYY (HH:mm)');
                            return M;
                        }
                    }
                ],
                "searching": true,
                "bLengthChange": true,
                "autoWidth": false,
                "destroy": true,
                "processing": true,
                //"serverSide": true,
                "filter": false
            });
        });

        $('#blmIntan').click(function () {
            $("#m_modal_vp").modal({
                keyboard: false,
                backdrop: "static"
            });
            $("#m_modal_vp_title").text("Vessel Progress : Request Today | Tanjung Intan");
        });
    }

    return {
        init: function () {
            pilotvesselprogEmas();
            pilotvesselprogIntan();
            pilotpfthemonthEmas();
            pilotpfthemonthIntan();
            RequestWTPEmas();
            RequestWTPIntan();

            vp_reqtoday();
        }
    };
}();

jQuery(document).ready(function () {
    Index.init();
    setInterval(function () {
        pilotvesselprogEmas();
        pilotvesselprogIntan();
        pilotpfthemonthEmas();
        pilotpfthemonthIntan();
        RequestWTPEmas();
        RequestWTPIntan();
    }, 600000);
});