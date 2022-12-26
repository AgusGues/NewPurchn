//== Class definition
var dash_odb = function () {

    var init_chart_perak = function () {
        var attr1 = 20;
        var attr2 = 30;
        var attr3 = 50;

        $("#perak_attr1_text").text(attr1);
        $("#perak_attr2_text").text(attr2);
        $("#perak_attr3_text").text(attr3);


        if ($('#chart_perak').length == 0) {
            return;
        }

        var chart = new Chartist.Pie('#chart_perak', {
            series: [{
                value: attr1,
                className: 'custom',
                meta: {
                    color: mUtil.getColor('success')
                }
            },
            {
                value: attr2,
                className: 'custom',
                meta: {
                    color: mUtil.getColor('warning')
                }
            },
            {
                value: attr3,
                className: 'custom',
                meta: {
                    color: mUtil.getColor('danger')
                }
            }
            ],
            labels: [1, 2, 3]
        }, {
                donut: true,
                donutWidth: 20,
                showLabel: false
            });

        chart.on('draw', function (data) {
            if (data.type === 'slice') {
                // Get the total path length in order to use for dash array animation
                var pathLength = data.element._node.getTotalLength();

                // Set a dasharray that matches the path length as prerequisite to animate dashoffset
                data.element.attr({
                    'stroke-dasharray': pathLength + 'px ' + pathLength + 'px'
                });

                // Create animation definition while also assigning an ID to the animation for later sync usage
                var animationDefinition = {
                    'stroke-dashoffset': {
                        id: 'anim' + data.index,
                        dur: 1000,
                        from: -pathLength + 'px',
                        to: '0px',
                        easing: Chartist.Svg.Easing.easeOutQuint,
                        // We need to use `fill: 'freeze'` otherwise our animation will fall back to initial (not visible)
                        fill: 'freeze',
                        'stroke': data.meta.color
                    }
                };

                // If this was not the first slice, we need to time the animation so that it uses the end sync event of the previous animation
                if (data.index !== 0) {
                    animationDefinition['stroke-dashoffset'].begin = 'anim' + (data.index - 1) + '.end';
                }

                // We need to set an initial value before the animation starts as we are not in guided mode which would do that for us

                data.element.attr({
                    'stroke-dashoffset': -pathLength + 'px',
                    'stroke': data.meta.color
                });

                // We can't use guided mode as the animations need to rely on setting begin manually
                // See http://gionkunz.github.io/chartist-js/api-documentation.html#chartistsvg-function-animate
                data.element.animate(animationDefinition, false);
            }
        });

        reload();
        // For the sake of the example we update the chart every time it's created with a delay of 8 seconds
        //chart.on('created', function () {
        //    if (window.__anim21278907124) {
        //        clearTimeout(window.__anim21278907124);
        //        window.__anim21278907124 = null;
        //    }
        //    window.__anim21278907124 = setTimeout(chart.update.bind(chart), 15000);
        //});
    }

    var init_chart_gresik = function () {
        var attr1 = 20;
        var attr2 = 30;
        var attr3 = 50;

        $("#gresik_attr1_text").text(attr1);
        $("#gresik_attr2_text").text(attr2);
        $("#gresik_attr3_text").text(attr3);


        if ($('#chart_gresik').length == 0) {
            return;
        }

        var chart = new Chartist.Pie('#chart_gresik', {
            series: [{
                value: attr1,
                className: 'custom',
                meta: {
                    color: mUtil.getColor('success')
                }
            },
            {
                value: attr2,
                className: 'custom',
                meta: {
                    color: mUtil.getColor('warning')
                }
            },
            {
                value: attr3,
                className: 'custom',
                meta: {
                    color: mUtil.getColor('danger')
                }
            }
            ],
            labels: [1, 2, 3]
        }, {
                donut: true,
                donutWidth: 20,
                showLabel: false
            });

        chart.on('draw', function (data) {
            if (data.type === 'slice') {
                // Get the total path length in order to use for dash array animation
                var pathLength = data.element._node.getTotalLength();

                // Set a dasharray that matches the path length as prerequisite to animate dashoffset
                data.element.attr({
                    'stroke-dasharray': pathLength + 'px ' + pathLength + 'px'
                });

                // Create animation definition while also assigning an ID to the animation for later sync usage
                var animationDefinition = {
                    'stroke-dashoffset': {
                        id: 'anim' + data.index,
                        dur: 1000,
                        from: -pathLength + 'px',
                        to: '0px',
                        easing: Chartist.Svg.Easing.easeOutQuint,
                        // We need to use `fill: 'freeze'` otherwise our animation will fall back to initial (not visible)
                        fill: 'freeze',
                        'stroke': data.meta.color
                    }
                };

                // If this was not the first slice, we need to time the animation so that it uses the end sync event of the previous animation
                if (data.index !== 0) {
                    animationDefinition['stroke-dashoffset'].begin = 'anim' + (data.index - 1) + '.end';
                }

                // We need to set an initial value before the animation starts as we are not in guided mode which would do that for us

                data.element.attr({
                    'stroke-dashoffset': -pathLength + 'px',
                    'stroke': data.meta.color
                });

                // We can't use guided mode as the animations need to rely on setting begin manually
                // See http://gionkunz.github.io/chartist-js/api-documentation.html#chartistsvg-function-animate
                data.element.animate(animationDefinition, false);
            }
        });

        reload();
        // For the sake of the example we update the chart every time it's created with a delay of 8 seconds
        //chart.on('created', function () {
        //    if (window.__anim21278907124) {
        //        clearTimeout(window.__anim21278907124);
        //        window.__anim21278907124 = null;
        //    }
        //    window.__anim21278907124 = setTimeout(chart.update.bind(chart), 15000);
        //});
    }

    var init_chart_probolinggo = function () {
        var attr1 = 20;
        var attr2 = 30;
        var attr3 = 50;

        $("#probolinggo_attr1_text").text(attr1);
        $("#probolinggo_attr2_text").text(attr2);
        $("#probolinggo_attr3_text").text(attr3);


        if ($('#chart_probolinggo').length == 0) {
            return;
        }

        var chart = new Chartist.Pie('#chart_probolinggo', {
            series: [{
                value: attr1,
                className: 'custom',
                meta: {
                    color: mUtil.getColor('success')
                }
            },
            {
                value: attr2,
                className: 'custom',
                meta: {
                    color: mUtil.getColor('warning')
                }
            },
            {
                value: attr3,
                className: 'custom',
                meta: {
                    color: mUtil.getColor('danger')
                }
            }
            ],
            labels: [1, 2, 3]
        }, {
                donut: true,
                donutWidth: 20,
                showLabel: false
            });

        chart.on('draw', function (data) {
            if (data.type === 'slice') {
                // Get the total path length in order to use for dash array animation
                var pathLength = data.element._node.getTotalLength();

                // Set a dasharray that matches the path length as prerequisite to animate dashoffset
                data.element.attr({
                    'stroke-dasharray': pathLength + 'px ' + pathLength + 'px'
                });

                // Create animation definition while also assigning an ID to the animation for later sync usage
                var animationDefinition = {
                    'stroke-dashoffset': {
                        id: 'anim' + data.index,
                        dur: 1000,
                        from: -pathLength + 'px',
                        to: '0px',
                        easing: Chartist.Svg.Easing.easeOutQuint,
                        // We need to use `fill: 'freeze'` otherwise our animation will fall back to initial (not visible)
                        fill: 'freeze',
                        'stroke': data.meta.color
                    }
                };

                // If this was not the first slice, we need to time the animation so that it uses the end sync event of the previous animation
                if (data.index !== 0) {
                    animationDefinition['stroke-dashoffset'].begin = 'anim' + (data.index - 1) + '.end';
                }

                // We need to set an initial value before the animation starts as we are not in guided mode which would do that for us

                data.element.attr({
                    'stroke-dashoffset': -pathLength + 'px',
                    'stroke': data.meta.color
                });

                // We can't use guided mode as the animations need to rely on setting begin manually
                // See http://gionkunz.github.io/chartist-js/api-documentation.html#chartistsvg-function-animate
                data.element.animate(animationDefinition, false);
            }
        });

        reload();
        // For the sake of the example we update the chart every time it's created with a delay of 8 seconds
        //chart.on('created', function () {
        //    if (window.__anim21278907124) {
        //        clearTimeout(window.__anim21278907124);
        //        window.__anim21278907124 = null;
        //    }
        //    window.__anim21278907124 = setTimeout(chart.update.bind(chart), 15000);
        //});
    }

    var init_chart_kalianget = function () {
        var attr1 = 20;
        var attr2 = 30;
        var attr3 = 50;

        $("#kalianget_attr1_text").text(attr1);
        $("#kalianget_attr2_text").text(attr2);
        $("#kalianget_attr3_text").text(attr3);


        if ($('#chart_kalianget').length == 0) {
            return;
        }

        var chart = new Chartist.Pie('#chart_kalianget', {
            series: [{
                value: attr1,
                className: 'custom',
                meta: {
                    color: mUtil.getColor('success')
                }
            },
            {
                value: attr2,
                className: 'custom',
                meta: {
                    color: mUtil.getColor('warning')
                }
            },
            {
                value: attr3,
                className: 'custom',
                meta: {
                    color: mUtil.getColor('danger')
                }
            }
            ],
            labels: [1, 2, 3]
        }, {
                donut: true,
                donutWidth: 20,
                showLabel: false
            });

        chart.on('draw', function (data) {
            if (data.type === 'slice') {
                // Get the total path length in order to use for dash array animation
                var pathLength = data.element._node.getTotalLength();

                // Set a dasharray that matches the path length as prerequisite to animate dashoffset
                data.element.attr({
                    'stroke-dasharray': pathLength + 'px ' + pathLength + 'px'
                });

                // Create animation definition while also assigning an ID to the animation for later sync usage
                var animationDefinition = {
                    'stroke-dashoffset': {
                        id: 'anim' + data.index,
                        dur: 1000,
                        from: -pathLength + 'px',
                        to: '0px',
                        easing: Chartist.Svg.Easing.easeOutQuint,
                        // We need to use `fill: 'freeze'` otherwise our animation will fall back to initial (not visible)
                        fill: 'freeze',
                        'stroke': data.meta.color
                    }
                };

                // If this was not the first slice, we need to time the animation so that it uses the end sync event of the previous animation
                if (data.index !== 0) {
                    animationDefinition['stroke-dashoffset'].begin = 'anim' + (data.index - 1) + '.end';
                }

                // We need to set an initial value before the animation starts as we are not in guided mode which would do that for us

                data.element.attr({
                    'stroke-dashoffset': -pathLength + 'px',
                    'stroke': data.meta.color
                });

                // We can't use guided mode as the animations need to rely on setting begin manually
                // See http://gionkunz.github.io/chartist-js/api-documentation.html#chartistsvg-function-animate
                data.element.animate(animationDefinition, false);
            }
        });

        reload();
        // For the sake of the example we update the chart every time it's created with a delay of 8 seconds
        //chart.on('created', function () {
        //    if (window.__anim21278907124) {
        //        clearTimeout(window.__anim21278907124);
        //        window.__anim21278907124 = null;
        //    }
        //    window.__anim21278907124 = setTimeout(chart.update.bind(chart), 15000);
        //});
    }

    var reload = function () {
        setTimeout(function () {
            init_chart_perak();
            init_chart_gresik();
            init_chart_probolinggo();
            init_chart_kalianget();
        }, 10000);
    }

    return {
        init: function () {
            init_chart_perak();
            init_chart_gresik();
            init_chart_probolinggo();
            init_chart_kalianget();
            reload();
        }
    }

}();

//== Class initialization on page load
jQuery(document).ready(function () {
    dash_odb.init();
});