//var tugging = function () {

//    var initdata = function () {
       
//        var datapilotvessel = localStorage.getItem("pilotvessel");
//        var pilotweek = localStorage.getItem("pilotweek");
//        var wtppilot = localStorage.getItem("pilotwtp");
//        var pilotservices = localStorage.getItem("Pilotservices");

//        if (pilotservices) {
//            var data = JSON.parse(pilotservices);
//            settuggingservices(data);
//        }
       
//        if (wtppilot) {
//            var data = JSON.parse(wtppilot);
//            initdatgeage(data);
//        } 
//        if (datapilotvessel) {
//            var data = JSON.parse(datapilotvessel);
//            $('#blm').html(data.BLM_PNTPN);
//            $('#ohn').html(data.OHN);
//            $('#pob').html(data.POB);
//        } 
//        if (pilotweek) {
//            var data = JSON.parse(pilotweek);
//            $('#namep').html(data.NAMA);
//            $('#nipp').html(data.NIPP);
//        } 
       
//        initreload();
        
//    }

//    var initreload = function () {
//        $.ajax({
//            url: "/Pilotservices/pilotvessel",
//            method: "GET",
//            dataType: "json",
//            async: true,
//            success: function (msgd) {
//                $('#blm').html(msgd.BLM_PNTPN);
//                $('#ohn').html(msgd.OHN);
//                $('#pob').html(msgd.POB);
//                localStorage.removeItem("pilotvessel");
//                localStorage.setItem("pilotvessel", JSON.stringify(msgd));

//            }
//        });
//        $.ajax({
//            url: "/Pilotservices/wtpservices",
//            method: "GET",
//            dataType: "json",
//            async: true,
//            success: function (msgd) {
//                initdatgeage(msgd);
//                localStorage.removeItem("pilotwtp");
//                localStorage.setItem("pilotwtp", JSON.stringify(msgd));

//            }
//        });
//        $.ajax({
//            url: "/Pilotservices/pilotofweekitem",
//            method: "GET",
//            dataType: "json",
//            async: true,
//            success: function (msgd) {
//                $('#namep').html(msgd.NAMA_PETUGAS);
//                $('#nipp').html(msgd.NIP_PANDU);
//                localStorage.removeItem("pilotweek");
//                localStorage.setItem("pilotweek", JSON.stringify(msgd));
//            }
//        });
//        $.ajax({
//            url: "/Pilotservices/Pilotservices",
//            method: "GET",
//            dataType: "json",
//            async: true,
//            success: function (msgd) {
//                fadeout();
//                settuggingservices(msgd);
//                localStorage.removeItem("Pilotservices");
//                localStorage.setItem("Pilotservices", JSON.stringify(msgd));
//            }
//        });
//    }

//    var initdatgeage = function(xdata) {

//        var dom = document.getElementById("psf");
//        var myChart = echarts.init(dom);
//        var app = {};
//        option = null;
//        option = {
//            tooltip: {
//                formatter: "{a} <br/>{b} : {c}%"
//            },
//            series: [{
//                name: 'Pilot Service Performance',
//                type: 'gauge',
//                detail: {
//                    formatter: '{value} minutes'
//                },
//                data: [{
//                    value: xdata.WTP,
//                }],
//                startAngle: 180,
//                endAngle: 0,
//                axisLine: {
//                    lineStyle: {
//                        width: 110,
//                        color: [
//                            [(xdata.EXCELLENT/100), '#50D850'],
//                            [(xdata.FAIR/100), '#00C8FF'],
//                            [(xdata.POOR/100), '#ED4D49']
//                        ]
//                    }
//                },
//                axisTick: {
//                    splitNumber: 10,
//                    length: 5
//                },
//                axisLabel: {
//                    formatter: function (a) {
//                        switch (a + "") {
//                            case "10":
//                                return "EXCELLENT";
//                            case "50":
//                                return "FAIR";
//                            case "90":
//                                return "POOR";
//                            default:
//                                return ""
//                        }
//                    },
//                    textStyle: {
//                        color: "#fff",
//                        fontSize: 12,
//                        fontWeight: "bold"
//                    }
//                },
//                pointer: {
//                    width: 30,
//                    length: "70%",
//                    color: "#FFF"
//                },
//                detail: {
//                    show: true,
//                    borderWidth: 0,
//                    fontSize: 25,
//                    formatter: "{value} minutes",
//                    offsetCenter: [5, -40],
//                    // color:'red',
//                },
//                itemStyle: {
//                    color: true,
//                    opacity: 0.8
//                },
//                radius: '180%',
//                center: ['50%', '100%'],



//            }]
//        };

//        // setInterval(function () {
//        //     option.series[0].data[0].value = (Math.random() * 100).toFixed(2) - 0;
//        //     myChart.setOption(option, true);
//        // }, 5000);
//        if (option && typeof option === "object") {
//            myChart.setOption(option, true);
//        }

//        //setInterval(function () {
//        //    option.series[0].data[0].value = (Math.random() * 100).toFixed(2) - 0;
//        //    myChart.setOption(option, true);
//        //}, 2000);
//    }

//    var reload = function () {
//        setInterval(function () {
//            initreload();
            
//        }, 20000);
//    }

//    var settuggingservices = function (xdata) {
//        var listrow = "";
//        var rows = "";
//        $.each(xdata.header, function(i, item) {
//            $.each(xdata.data, function(s, items){
//                if(items.KD_LOKASI==item.KD_LOKASI){
//                rows+='<div class="col"> '+
//                    '<label class="fs-20 fw-700">'+ items.STATUS_NAMA + '</label>'+
//                    '<p class="fs-35 fw-700 p-2">'+items.JUMLAH+'</p>'+
//                '</div>';
//                }

//            });
//            rows +='</div>';
//            listrow +=rows;
//        })
//        $('#avalibeltugging').html(xdata.jumlah);
//        $('#content-tugging').html(listrow);
//        setuppilotavbl(xdata.avbl);
//        tugg(xdata.tugg);
//        fadein();
//    }

//    var setuppilotavbl = function (xdata) {
//        var listrow = "";
//        $.each(xdata.ATVTS, function (i, item) {
//            var row;
//            $.each(item.DATA, function (x, items) {
                
//                         row = '<div class="col-lg-3">' +
//                                    '<label class="fs-20 fw-700 '+rederbadge(items.ID)+'  text-white w-100 curved">' + items.NAMA + '</label>' +
//                                    '<p class="fs-35 fw-700 p-2 '+redertext(items.ID)+'">'+items.JUMLAH+'</p>' +
//                                '</div>';
//                    });
//            row += '</div>';
//            listrow += row;
           

//        });
//        $('#avblpilot').html(xdata.AVBL);
//        $('#content-pilot').html(listrow);


//        function rederbadge(c) {
//            if (c == 1) {
//                return 'bg-primary'
//            } else if (c == 2) {
//                return 'bg-warning'
//            }else if(c == 3){
//                return 'badge-success'
//            }
//        }

//        function redertext(c) {
//            if (c == 1) {
//                return 'text-primary'
//            } else if (c == 2) {
//                return 'text-warning'
//            } else if (c == 3) {
//                return 'text-success'
//            }
//        }
       
        
//    }

//    var tugg = function (xdata) {

//        var header = '<thead class="fs-30">' +
//                            '<tr>' +
//                               ' <th class="my-auto" style="/*vertical-align: center;line-height: 90px*/">Jam</th>';
//                $.each(xdata.header, function (i, itmss) {
//                    header += '<th>' + itmss.NAMA_KAWASAN+'</th>'
//                });
//                header += '</tr> </thead>';
//                var tbody = '<tbody class="fs-40 ">';
//                $.each(xdata.jam, function (x, itms2) {
//                    tbody += '<tr>' + '<td>' + itms2 + '</td>';

//                    $.each(xdata.data, function (c, citems) {
//                        tbody += '<td><span class="badge badge-pill ' + rederwarnig(citems[x].BLINK) + '">' + citems[x].JUMLAH_KPL_TUNDA + '</span></td>';
//                    });
//                    tbody +='</tr>';                    
//                });
//                tbody += '</tbody>';

//                $('#tug-data').html(header + tbody);
                                                                     
//                function rederwarnig(c) {
//                    if (c == 1) {
//                        return 'badge-danger  blink'
//                    } else{
//                        return 'badge-success';
//                    }
//                }
                       
//    }


//    var fadein = function () {
//        fadeout();
//        setTimeout(function () {
//            $('.animation-demo').dreyanim({
//                animationType: 'wipeInVertical',
//                animationTime: 2000
//            });
//        }, 2000);
        
      
//    }

//    var fadeout = function () {

//        $('.animation-demo').dreyanim({
//            animationType: 'wipeOutVertical',
//            animationTime: 2000
//        });

//    }



   

//    return {
//        init: function () {
//            initdata();
            
           
           

//        }
//    };
//}();


//jQuery(document).ready(function () {
//    tugging.init();
//});