    var Header = "ACR"
    String.prototype.reverse=function() {return this.split("").reverse().join("");}
    function RemoveDecimal(s) {
        if (typeof s === 'string') {
            if (s.includes('.')) {
                return parseInt(s.split('.')[0])
            }
            console.error("No decimal-like pattern found.")
        }
        else if (typeof s === 'number') {
            s = toString(s);
            if (s.includes('.')) {
                return parseInt(s.split('.')[0])
            }
            console.error("No decimal-like pattern found.")
        }
        return 0
    }
    function RandomDecimal() {return Math.random() * 2}
    function GetFactors(key) {
        var c1 = key.charAt(0).charCodeAt(0) * 2 ;
        var c2 = key.slice(-1).charCodeAt(0) * 3 ; 
        return [c1, c2];
    }
    function GetMath(key) {
        var result = 0
        for (var x = 0; x < key.length; x++)
        {
            result += key.charAt(x).charCodeAt(0) * key.length * GetFactors(key)[0] * GetFactors(key)[1];
        }
        return result
    }
    module.exports.Encrypt = function (text,key) {
        var result = []
        for (var x = 0; x < text.length; x++)
        {
            var EncryptionMath = text.charAt(x).charCodeAt(0) * GetMath(key);
            result.push( EncryptionMath + RandomDecimal())
        }
        var ToReturn = Header + "/" + result.join("/")
        return ToReturn
    }
    module.exports.Decrypt = function(text,key) {
        if (text.startsWith(Header)) {
            var EncryptionMath = GetMath(key)
            var DecryptionResult = []
            var NewArray = text.split('/')
            NewArray.shift()
            for (let i of NewArray)
            {
                i = RemoveDecimal(i)
                DecryptionResult.push(String.fromCharCode(i / EncryptionMath))
            }
            return DecryptionResult.join('')
        }
        return "INVALID_CIPHERTEXT"
    }

