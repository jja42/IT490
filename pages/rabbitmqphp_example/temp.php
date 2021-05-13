<?php
$a = password_hash("Mypass", CRYPT_BLOWFISH);
$b = password_hash("Mypass", CRYPT_BLOWFISH);
echo "Here: $a\n";
echo "There: $b\n";

if(hash_equals($a, $b)) echo "EQUALS!!\n";
else echo "DNE :(\n";
