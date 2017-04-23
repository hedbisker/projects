define([],function(){
    return function () {
        this.drawPaper = null;
        return {
            draw: function () {
                this.drawPaper();
            },
            setCP: function (_drawPaper) {
                this.drawPaper = _drawPaper;
            },
        };
    };
})
