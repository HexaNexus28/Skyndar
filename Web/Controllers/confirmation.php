<?php
require '../Models/userdata.php';
require '../Models/creneaudata.php';
require '../vendor/autoload.php';


use PHPMailer\PHPMailer\PHPMailer;
use PHPMailer\PHPMailer\Exception;
use Dotenv\Dotenv;

// Charger les variables d'environnement
$dotenv = Dotenv::createImmutable(__DIR__ . '/../../');
$dotenv->load();


$prestataireEmail = 'testcodelily@gmail.com'; //Test pour voir si le prestataire reçoit le mail
$prestataireNom = 'Lily';


if ($_SERVER['REQUEST_METHOD'] === 'GET') {
    if (isset($_GET['nom'], $_GET['prenom'], $_GET['email'], $_GET['creneau_id'])) {
        $nom = htmlspecialchars($_GET['nom']);
        $prenom = htmlspecialchars($_GET['prenom']);
        $clientemail = htmlspecialchars($_GET['email']);
        $creneau_id = (int) $_GET['creneau_id'];

        $user_id = getuserid($nom, $clientemail);

        if ($user_id && $creneau_id) {
            insertrendezvous($user_id, $creneau_id);
        } else {
            echo "Erreur lors de la création de l'utilisateur.";
            exit;
        }

        // Récupère le créneau pour l'affichage
        $creneau = getcreneaubyid($creneau_id);
        if (!$creneau) {
            echo "Créneau non trouvé.";
            exit;
        }


        $heure_debut = $creneau['starthour'] ?? 'Heure inconnue';
        $heure_fin = $creneau['endhour'] ?? 'Heure inconnue';
        $lieu = "L'Embelie, 13 rue Pottier, Le Chesnay-Rocquencourt";
        $date = $creneau['date'];
        $prestation = $creneau['prestation_id'];
        try {
            $mailClient = new PHPMailer(true);
            $mailClient->CharSet = 'UTF-8';
            $mailClient->Encoding = 'base64';
            $mailClient->SMTPDebug = 0;
            $mailClient->isSMTP();
            $mailClient->Host = $_ENV['SMTP_HOST'];
            $mailClient->SMTPAuth = true;
            $mailClient->Username = $_ENV['SMTP_USERNAME'];
            $mailClient->Password = $_ENV['SMTP_PASSWORD'];
            $mailClient->SMTPSecure = PHPMailer::ENCRYPTION_STARTTLS;
            $mailClient->Port = $_ENV['SMTP_PORT'];
            $mailClient->setFrom($_ENV['SMTP_FROM'], $_ENV['SMTP_NAME']);

            $mailClient->addAddress($clientemail, $prenom);
            $mailClient->isHTML(true);
            $mailClient->Subject = 'Confirmation de rendez-vous';
            $mailClient->Body = "Bonjour,<br><br>"
                . "Merci pour votre réservation. Voici les détails de votre rendez-vous: <br>"
                . "<strong>Date :</strong> $date<br>"
                . "<strong>Heure :</strong> $heure_debut - $heure_fin <br>"
                . "<strong>Lieu :</strong> $lieu<br><br>"
                . "<strong>Prestation :</strong> $prestation<br>"
                . "Je vous remercie pour votre confiance. <br><br>"
                . "📩 Pour toute modification ou annulation, merci de me contacter par e-mail à : <a href='mailto:contact@e-ki-libre.net'>contact@e-ki-libre.net</a><br>"
                . "📄 Consultez la politique d’annulation/modification ici : <a href='https://e-ki-libre.net/tarifs/'>https://e-ki-libre.net/tarifs/</a><br>"

                . "À très bientôt, <br>"
                . "Bertrand MANGIN";
            $mailClient->send();
        } catch (Exception $e) {
            echo "Le mail n'a pas pu être envoyé. Mail Erreur: {$mailClient->ErrorInfo}";
        }


        try {
            $mailPro = new PHPMailer(true);
            $mailPro->CharSet = 'UTF-8';
            $mailPro->Encoding = 'base64';
            $mailPro->SMTPDebug = 0;
            $mailPro->isSMTP();
            $mailPro->Host = $_ENV['SMTP_HOST'];
            $mailPro->SMTPAuth = true;
            $mailPro->Username = $_ENV['SMTP_USERNAME'];
            $mailPro->Password = $_ENV['SMTP_PASSWORD'];
            $mailPro->SMTPSecure = PHPMailer::ENCRYPTION_STARTTLS;
            $mailPro->Port = $_ENV['SMTP_PORT'];
            $mailPro->setFrom($_ENV['SMTP_FROM'], $_ENV['SMTP_NAME']);

            $mailPro->addAddress($prestataireEmail, $prestataireNom);

            $mailPro->isHTML(true);
            $mailPro->Subject = 'Nouveau rendez-vous réservé';
            $mailPro->Body = "Un nouveau RDV vient d’être réservé !<br><br>"
                . "<strong>Client :</strong> $prenom $nom<br>"
                . "<strong>Email :</strong> $clientemail<br>"
                . "<strong>Date :</strong> $date<br>"
                . "<strong>Heure :</strong> $heure_debut - $heure_fin <br>"
                . "<strong>Lieu :</strong> $lieu<br><br>"
                . "Merci de bien vouloir préparer ce créneau.";

            $mailPro->send();

        } catch (Exception $e) {
            echo "Le mail n'a pas pu être envoyé. Erreur : {$mailPro->ErrorInfo}";
        }

    } else {
        echo "Paramètres manquants.";
        exit;
    }
}

require '../Views/confirmation.php';