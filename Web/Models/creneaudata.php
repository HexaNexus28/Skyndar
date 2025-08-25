<?php
require 'connexion.php';
function getcreneaux($prestation_id, $day_id)
{
    global $db;
    $query = $db->prepare('SELECT * FROM creneau WHERE prestation_id = :prestation_id AND day_id = :day_id order by starthour ASC');
    $query->bindParam(':prestation_id', $prestation_id);
    $query->bindParam(':day_id', $day_id);

    $query->execute();
    return $query->fetchAll();

}
function getreservedcreneaux($creneau_id)
{
    global $db;
    $query = $db->prepare('SELECT cr.starthour FROM rendezvous r join creneau cr on r.creneau_id = cr.id WHERE cr.id = :creneau_id');
    $query->bindParam(':creneau_id', $creneau_id);
    $query->execute();
    return $query->fetchAll();

}
function getcreneaubyid($creneau_id)
{
    global $db;
    $query = $db->prepare('SELECT cr.* ,p.*, c.date FROM creneau AS cr JOIN calendarday AS c ON cr.day_id = c.id JOIN prestation AS p ON p.id = cr.prestation_id WHERE cr.id = :creneau_id');
    $query->bindParam(':creneau_id', $creneau_id);

    $query->execute();
    return $query->fetch();
}
function getday_id($date)
{
    global $db;
    $query = $db->prepare('SELECT id FROM calendarday WHERE date = :date');
    $query->bindParam(':date', $date);

    $query->execute();
    $result = $query->fetch();
    if ($result) {
        return $result['id'];
    } else {
        return 'not found';
    }
}

function generatecalendar($currentyear, $currentmonth)
{
    $calendar = array_fill(0, 42, null);

    $firstDayOfMonth = date('N', strtotime("$currentyear-$currentmonth-01"));
    $daysInMonth = cal_days_in_month(CAL_GREGORIAN, $currentmonth, $currentyear);

    for ($day = 1; $day <= $daysInMonth; $day++) {
        $date = sprintf('%04d-%02d-%02d', $currentyear, $currentmonth, $day);
        $dayIndex = $firstDayOfMonth - 1 + $day - 1;

        $calendar[$dayIndex] = [
            'date' => $date,
            'day' => $dayIndex,
            'day_of_week' => date('N', strtotime($date)),
            'is_valid' => true
        ];
    }

    return $calendar;
}