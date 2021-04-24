if string.split == nil then --Vanilla Lua
    string.split = function(str, delimit)
        local result = {};
        for match in (str..delimit):gmatch("(.-)"..delimit) do
             table.insert(result, match);
        end
        return result;
    end
end
string.startsWith = function(str, pattern)
    local SubString = str:sub(0, #pattern)
    if  SubString == pattern then 
        return true
    end
    return false
end
local ACrypt = {
    Header = "ACR";
    __VERSION = "1.1";
    __AUTHOR = "MyMomThinksImGay#1842"
}
function ACrypt:__RandomDecimal()
    return  tonumber("0."..tostring(math.random(0,999999)))
end
function ACrypt:__RemoveDecimal(any)
    local ErrorMessage = "No decimal-like pattern found."
    if typeof(any) == "string" then 
        assert(any:find("."), ErrorMessage)
        local new = string.split(any, ".")
        return tonumber(new[1])
    elseif typeof(any) == "number" then 
        any = tostring(any)
        assert(any:find("."), ErrorMessage)
        local new = string.split(any, ".")
        return tonumber(new[1])
    end
    return nil
end
function ACrypt:__GetFactors(str)
    local c1 = str:sub(1, 1):byte() * 2 --First Char
    local c2 = str:sub(-2, -1):byte() * 3 --Last Char
    local tmp = {c1,c2}
    return tmp
end
function ACrypt:__GetMath(key)
    local result = 0
    local returns = ACrypt:__GetFactors(key)
    for i in key:gmatch(".") do 
        result = result + (i:byte() * #key * returns[1] * returns[2])
    end
    return result
end
function ACrypt:Encrypt(text, cipherkey)
    local result = {}
    for i in text:gmatch(".") do 
        local EncryptionMath = i:byte() * ACrypt:__GetMath(cipherkey)
        table.insert(result, EncryptionMath + ACrypt:__RandomDecimal())
    end
    local ToReturn = ACrypt.Header.."/"..table.concat(result, "/")
    return ToReturn
end

function ACrypt:Decrypt(text, cipherkey)
    if string.startsWith(text, ACrypt.Header) then 
        local NewTable = string.split(text, "/")
        local DecryptionResult = {}
        local Math = ACrypt:__GetMath(cipherkey)
        table.remove(NewTable,1) --Remove the Header
        for __,v in pairs(NewTable) do 
            v = ACrypt:__RemoveDecimal(v)
            table.insert(DecryptionResult,string.char(v / Math))
        end
        return table.concat(DecryptionResult, "")
    end
    return "INVALID_CIPHERTEXT"
end



return ACrypt;