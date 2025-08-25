<?php
require '../Models/userdata.php';
if ($_SERVER['REQUEST_METHOD']=== 'GET'){
    if (isset($_GET['creneau_id'])) {
        $creneau_id = (int)$_GET['creneau_id'];
        
    }
    else {
        $creneau_id = 1;
    }
}
require '../Views/formulaire.php';