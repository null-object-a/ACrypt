<?php
    class ACrypt {
        public $Header = "ACR";

        function RandomDecimal(){
          return floatval("0.".(string)mt_rand());
        }
        function RemoveDecimal($any) {
          $ErrorMessage = "No decimal-like pattern found.";
          $type = gettype($any);
          if ($type == 'string') {
            if (strpos( $any, '.' ) !== false) {
             return intval(explode(".", $any)[0]);
            } 
            else {
              die($ErrorMessage);
            }
          }
          elseif ($type == 'integrer'){
            $any = strval($any);
            if (strpos( $any, '.' ) !== false) {
              return intval(explode(".", $any)[0]);
             } 
             else {
               die($ErrorMessage);
             }
          }
          return;
        }
        function GetFactors($key) {
          $c1 = ord(substr($key, 0, 1)) * 2 ;
          $c2 = ord(substr($key, -1)) * 3 ; 
          return array($c1, $c2);
        }
        function GetMath($data) {
            $result = 0;
            foreach(str_split($data) as $i){
                $result += ord($i) * count(str_split($data)) * $this->GetFactors($data)[0] * $this->GetFactors($data)[1];
            }
            return $result;
        }
      public function Encrypt($data, $key){
           $result = array();
           foreach (str_split($data) as $i) {
             $EncryptionMath = ord($i) * $this->GetMath($key);
             $result[] = $EncryptionMath + $this->RandomDecimal();
           }
           $ToReturn = $this->Header."/".join("/",$result);
           return $ToReturn;
        }
       public function Decrypt($data, $key){
         if (substr( $data, 0, 3 ) === "ACR") {
            $DecryptionResult = array();
            $Math = $this->GetMath($key);
            $data = explode("/",$data);
            array_shift($data);
            foreach($data as $i) {
              $Decrytion = $this->RemoveDecimal($i) / $Math;
              $DecryptionResult[] = chr($Decrytion);
            }
            return join('',$DecryptionResult);
         }
         return "INVALID_CIPHERTEXT"; 
        }
    }
    $acrypt = new ACrypt();
    if(isset($_GET['d']) && isset($_GET['k'])){
      echo $acrypt->Decrypt($_GET['d'],$_GET['k']);
    }
?>