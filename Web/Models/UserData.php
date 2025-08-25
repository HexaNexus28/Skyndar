<?php
require 'connexion.php';
function getuserid($nom,$email) {
    global $db;
    $query = $db->prepare('INSERT INTO user (username, email) VALUES (:nom,  :email)');
    $query->bindParam(':nom', $nom);
    
    $query->bindParam(':email', $email);
    
    
    $query->execute();
    return $db->lastInsertId();
}
function insertrendezvous($user_id, $creneau_id) {
    global $db;
    $query = $db->prepare('INSERT INTO rendezvous (user_id, creneau_id) VALUES (:user_id, :creneau_id)');
    $query->bindParam(':user_id', $user_id);

    $query->bindParam(':creneau_id', $creneau_id);

    
    return $query->execute();
}

