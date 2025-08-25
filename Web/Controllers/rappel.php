<?php
require '../vendor/autoload.php';
use PHPMailer\PHPMailer\PHPMailer;
use PHPMailer\PHPMailer\Exception;

$db = new PDO('mysql:host=localhost;dbname=skyndar;charset=utf8', 'root', '');

$query = $db->prepare("
    SELECT r.id, r.rendezvous_datetime, u.email, u.username, p.titre
    FROM rendezvous r
    JOIN user u ON u.id = r.user_id
    JOIN creneau c ON c.id = r.creneau_id
    JOIN prestation p ON p.id = c.prestation_id
    WHERE TIMESTAMPDIFF(MINUTE, NOW(), r.rendezvous_datetime) BETWEEN 1435 AND 1445
");
$query->execute();
$rdvs = $query->fetchAll(PDO::FETCH_ASSOC);

foreach ($rdvs as $rdv) {
    $mail = new PHPMailer(true);
    try {
        $mail->isSMTP();

        $mail->setFrom('zoglopiere20@gmail.com', 'Skyndar');
        $mail->addAddress($rdv['email'], $rdv['username']);

        $heureRDV = date('H:i', strtotime($rdv['rendezvous_datetime']));
        $dateRDV = date('d/m/Y', strtotime($rdv['rendezvous_datetime']));

        $mail->Subject = "Rappel - Votre RDV est demain à $heureRDV";
        $mail->Body = "Bonjour {$rdv['username']} !\n\n"
            . "Nous vous rappelons que votre rendez-vous approche. Voici un récapitulatif des informations :\n"
            . "Date : $dateRDV\n"
            . "Heure : $heureRDV\n"
            . "Lieu : {$creneau['cabinet']}\n"
            . "Prestation : {$rdv['titre']}\n\n"
            . "Je vous remercie à nouveau pour votre confiance et reste disponible si besoin. \n"
            . "A bientôt,\n"
            . "Bertrand MANGIN\n";

        $mail->send();
    } catch (Exception $e) {
        error_log("Erreur rappel mail 24h : " . $mail->ErrorInfo);
    }
}

require '../Models/CreneauData.php';
require '../Models/MailService.php';

    