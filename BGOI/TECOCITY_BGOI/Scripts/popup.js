/*
警告框、确认框、提示框
mht 2011/03/01 
*/
       var pop =null;
       function ShowIframe(title, contentUrl, width, height, event)
        {
            pop = new Popup({ contentType: 1, isReloadOnClose: false, width: width, height: height, event: event });
           pop.setContent("contentUrl",contentUrl);
           pop.setContent("title",title);
           pop.build();
           pop.show();
        }
        function ShowHtmlString(title, strHtml, isCursorRelative, isShowTitleBar,width, height,event)
        {
            pop = new Popup({ contentType: 2, isReloadOnClose: false, isCursorRelative: isCursorRelative, isShowTitleBar:isShowTitleBar, width: width, height: height, event:event });
            pop.setContent("contentHtml",strHtml);
            pop.setContent("title",title);
            pop.build();
            pop.show();
       }
       function ShowConfirm(title, confirmCon, okListenerId, okButtonText, okParam, cancelListenerId, cancelButtonText, cancelParam, width, height, event)
        {
            var pop=new Popup({ contentType:3,isReloadOnClose:false,width:width,height:height,event:event});
            pop.setContent("title",title);
            pop.setContent("confirmCon",confirmCon);
            pop.setContent("okCallBack", OKCallBack);
            pop.setContent("okListenerParameter", { listenerid: okListenerId, param: okParam, popObj: pop }); //添加确认框与外围交互的参数,str为备用参数
            pop.setContent("okButtonText", okButtonText);
            pop.setContent("cancelCallBack", CancelCallBack);
            pop.setContent("cancelListenerParameter", { listenerid: cancelListenerId, param: cancelParam, popObj: pop }); //添加确认框与外围交互的参数,str为备用参数
            pop.setContent("cancelButtonText", cancelButtonText);
            pop.build();
            pop.show();
        }
        
        //确认框“确定”按钮回调
        function OKCallBack(para) {
            var popObj = para["popObj"]
            var listener = document.getElementById(para["listenerid"]);
            popObj.close();
            if (listener != null) window.document.fireEvent("onclick",listener);
        }

        //确认框“取消”按钮回调
        function CancelCallBack(para) {
            var popObj = para["popObj"]
            var listener = document.getElementById(para["listenerid"]);
            popObj.close();
            if (listener != null) window.document.fireEvent("onclick", listener);
        }

        function ShowAlert(title, alertCon, yesButtonText, width, height, event)
        {
            pop = new Popup({ contentType: 4, isReloadOnClose: false, width: width, height: height, event: event });
            pop.setContent("title",title);
            pop.setContent("alertCon", alertCon);
            pop.setContent("yesButtonText", yesButtonText);
            pop.build();
            pop.show();
        }
        //关闭弹出框
        function ClosePop()
        {
	        if(pop != null)pop.close();
        }
