#!/usr/bin/env bash

# Updating Manifest

MANIFEST="$APPCENTER_SOURCE_DIRECTORY/GrampsView/GrampsView.Android/Properties/AndroidManifest.xml"

VERSIONNAME=`grep versionName ${MANIFEST} | sed 's/.*versionName\s*=\s*\"\([^\"]*\)\".*/\1/g'`

sed -i.bak "s/android:versionName="\"${VERSIONNAME}\""/android:versionName="\"5.0.${APPCENTER_BUILD_ID}\""/" ${MANIFEST}

rm -f ${MANIFEST}.bak

# Print out file for reference
cat $MANIFEST

echo 