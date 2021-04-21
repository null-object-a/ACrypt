setreadonly(string,false)
SUM = function(str) 
    local sum = 0
    for c in str:gmatch"." do
      sum = sum + c:byte()
    end
    return sum 
end
EncodeA = function(str,key) 
    local fields = { str:match( (str:gsub(".", "(.)")) ) }
    local rel = ""
    for i,v in pairs(fields) do
       rel = rel.."/"..tostring(v:byte() * key) 
    end
    return string.reverse(rel)
  end
DecodeA = function(str,key) 
    local tab = string.split(string.reverse(str), '/')
    local rel = ""
    for i,v in pairs(tab) do 
         if v == "" or v == " " then else
             rel=rel..string.char(tonumber(v/key))
         end
    end
    return rel
 end

EncodeB = function(str,key)
        local tmp = EncodeA(str,SUM(key))
        tmp = "[ACRYPT] "..string.reverse(tmp)
        return tmp
end

DecodeB = function(str,key) 
    if str:find("[ACRYPT] ") == 1 then
        if str:match("[\/]") then 
            local tmp = string.reverse(str:gsub("%[ACRYPT%] ", ""))
            return DecodeA(tmp,SUM(key))
        end
    end
end
