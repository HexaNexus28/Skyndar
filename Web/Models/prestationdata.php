<?php
require 'connexion.php';
function GetPrestations() {
    global $db;
    $query = $db->prepare('SELECT * FROM prestation ');
    
    $query->execute();
    return $query->fetchAll();
}