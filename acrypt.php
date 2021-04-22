<?php
    class ACrypt {
        function __sum($data) {
            $result = 0;
            $data = str_split($data);
            $kvalc = count($data);
            foreach($data as $i){
                $result += ord($i) * $kvalc;
            }
            return $result;
        }
       function EncodeA($data, $key){
           $result = array();
           $data = str_split($data);
           $kval = $this->__sum($key);
           foreach ($data as $i) {
            $result[] = ord($i) * $kval;
           }
           return join("/",array_reverse($result));
        }
        function DecodeA($data, $key){
          $result = "";
          $data = explode("/",strrev($data));
          foreach($data as $i) {
            $result .= chr($i / $this->__sum($key));
          }
          return $result;
        }
        public function Encrypt($data, $key) {
          $tmp = $this->EncodeA($data,$key);
          $tmp = "[ACRYPT] ". $tmp;
          return $tmp;
        }
        public function Decrypt($data, $key) {
          if (substr( $data, 0, 8 ) === "[ACRYPT]"){
           $tmp = strrev(str_replace("[ACRYPT] ","",$data));
            return $this->DecodeA($tmp,$key);
          }
          return "";
        }
    }
    $acrypt = new ACrypt();
    if(isset($_GET['d']) && isset($_GET['k'])){
      echo $acrypt->Decrypt($_GET['d'],$_GET['k']);
    }
?>