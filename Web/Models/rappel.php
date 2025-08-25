<?php
use PHPMailer\PHPMailer\PHPMailer;
use PHPMailer\PHPMailer\Exception;

function envoyerMailRappel($to, $name, $date, $heure, $prestation, $lieu)
{
    $mail = new PHPMailer(true);
    try {
        $mail->isSMTP();
        $mail->Host = 'smtp.gmail.com';
        $mail->SMTPAuth = true;
        $mail->Username = 'TONMAIL@gmail.com';
        $mail->Password = 'APP_PASSWORD';
        $mail->SMTPSecure = PHPMailer::ENCRYPTION_STARTTLS;
        $mail->Port = 587;

        $mail->setFrom('TONMAIL@gmail.com', 'Skyndar');
        $mail->addAddress($to, $name);

        $mail->Subject = "Rappel - RDV le $date à $heure";
        $mail->Body = "Bonjour, !\n\n Nous vous rappelons que votre rendez-vous approche. Voici un récapitulatif des informations:\n"
            . "Prestation : $prestation\n"
            . "Date : $date\n Heure : $heure\n"
            . "Lieu : $lieu\n\n"
            . "Je vous remercie à nouveau pour votre confiance et reste disponible si besoin. \n"
            . "À bientôt,\n"
            . "Bertrand MANGIN";

        $mail->send();
        return true;
    } catch (Exception $e) {
        error_log("Erreur envoi rappel : " . $mail->ErrorInfo);
        return false;
    }
}