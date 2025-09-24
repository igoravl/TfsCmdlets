#! /usr/bin/python

import uuid

terminalNamespaceGUID = uuid.UUID("{f65ddb7e-706b-4499-8a50-40313caf510a}")
appNamespaceGUID = uuid.uuid5(terminalNamespaceGUID, "TfsCmdlets".encode("UTF-16LE").decode("ASCII"))

profileGUID51 = uuid.uuid5(appNamespaceGUID, "Azure DevOps Shell (PS 5.1)".encode("UTF-16LE").decode("ASCII"))
profileGUID7x = uuid.uuid5(appNamespaceGUID, "Azure DevOps Shell (PS 7.x)".encode("UTF-16LE").decode("ASCII"))

print("Windows Terminal profile GUIDs")
print("==============================")
print("")
print(f"PS 5.1: {{{profileGUID51}}}")
print(f"PS 7.x: {{{profileGUID7x}}}")
