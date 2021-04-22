String.prototype.reverse=function() {return this.split("").reverse().join("");}
 function Sum(key) {
        var result = 0
        for (var x = 0; x < key.length; x++)
        {
            var c = key.charAt(x);
            result += c.charCodeAt(0) * key.length;
        }
        return result
    }
 function EncodeA(text,key) {
        var result = []
        for (var x = 0; x < text.length; x++)
        {
            var c = text.charAt(x);
            result.push(c.charCodeAt(0) * Sum(key))
        }
        return result.join("/").reverse()
    }
 function DecodeA(text,key) {
        var result = ""
        var t = text.reverse().split('/')
        //console.log(t)
        for (let i of t)
        {
           result += String.fromCharCode(parseInt(i)/Sum(key));
        }
        //console.log("DECODEA "+result)
        return result
    }
  function  encrypt(text,key){
        try {
		    var tmp = EncodeA(text,key)
            console.log(tmp)
            tmp = "[ACRYPT] "+tmp.reverse()
		    return tmp;
        } catch (err) {
            console.log(err);
            return err;
        }
    }

  function  decrypt(text,key){
        try {
            if (text.startsWith("[ACRYPT]")) {
                var tmp = text.replace("[ACRYPT] ","").reverse();
                return DecodeA(tmp,key)
            }
            return ""
        } catch(err) {
            return err;
        }
    }
    console.log(decrypt("[ACRYPT] 114492/99687/113505/114492","key"))