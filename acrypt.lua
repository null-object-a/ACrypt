--setreadonly(string,false) Uncomment this if you're using RLua
string.startsWith = function(orig,m) 
    return orig:find(m) == 1
end  
string.replace = function(orig,subs,rstr) 
    return orig:gsub(subs, rstr)
end
EncodeA = function(str) 
    local fields = { str:match( (str:gsub(".", "(.)")) ) }
    local rel = ""
    for i,v in pairs(fields) do
       rel = rel.."/"..tostring(v:byte()) 
    end
    return string.reverse(rel)
  end
DecodeA = function(str) 
    local tab = string.split(string.reverse(str), '/')
    local rel = ""
    for i,v in pairs(tab) do 
         if v == "" or v == " " then else
             rel=rel..string.char(tonumber(v))
         end
    end
    return rel
 end

EncodeB = function(str)
        local tmp = EncodeA(str)
        tmp = "[ACRYPT] "..string.reverse(tmp)
        return tmp
end

DecodeB = function(str) 
    if str:find("[ACRYPT] ") == 1 then
        --print("[DEBUG] Decrypting.")
        if str:match("[\/]") then 
           --print("[DEBUG] Decrypting stage 2.")
            local tmp = string.reverse(str:gsub("%[ACRYPT%] ", ""))
            return DecodeA(tmp)
        end
    end
end
 
