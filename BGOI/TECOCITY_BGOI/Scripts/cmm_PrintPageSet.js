var hkey_root, hkey_path, hkey_key;
hkey_root = "HKEY_CURRENT_USER";
hkey_path = "\\Software\\Microsoft\\Internet Explorer\\PageSetup\\";
function pagesetup(top, bottom, left, right) {
    var hkey_root, hkey_path, hkey_key;
    hkey_root = "HKEY_CURRENT_USER"
    hkey_path = "\\Software\\Microsoft\\Internet Explorer\\PageSetup\\";
    try {
        var RegWsh = new ActiveXObject("WScript.Shell");
        hkey_key = "header";
        RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "");
        hkey_key = "footer";
        RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "");
        hkey_key = "margin_bottom";
        //设置下页边距（0）      
        RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, bottom);
        hkey_key = "margin_left";
        //设置左页边距（0）      
        RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, left);
        hkey_key = "margin_right";
        //设置右页边距（0）      
        RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, right);
        hkey_key = "margin_top";
        //设置上页边距（8）      
        RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, top);
    } catch (e) { }
}