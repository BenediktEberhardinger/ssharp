# The MIT License (MIT)
# 
# Copyright (c) 2014-2016, Institute for Software & Systems Engineering
# 
# Permission is hereby granted, free of charge, to any person obtaining a copy
# of this software and associated documentation files (the "Software"), to deal
# in the Software without restriction, including without limitation the rights
# to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
# copies of the Software, and to permit persons to whom the Software is
# furnished to do so, subject to the following conditions:
# 
# The above copyright notice and this permission notice shall be included in
# all copies or substantial portions of the Software.
# 
# THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
# IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
# FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
# AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
# LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
# OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
# THE SOFTWARE.


# Note: You must run the following command first
#  Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
# To Undo
#  Set-ExecutionPolicy -ExecutionPolicy Restricted -Scope CurrentUser

# It is easy to get the method names in an assembly by extracting them from TestResult.xml which is generated by nunit.exe


###############################################
# Height Control
###############################################

# Original-Original-Original
AddTest -Testname "HeightControl_Original-Original-Original_Probability_HazardCollision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.Original-Original-Original" -TestNunitCategory "CollisionProbability" -TestCategories @("HeightControl","Variant-Original-Original-Original","Hazard-Collision","Probability")
AddTest -Testname "HeightControl_Original-Original-Original_Probability_HazardFalseAlarm" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.Original-Original-Original" -TestNunitCategory "FalseAlarmProbability" -TestCategories @("HeightControl","Variant-Original-Original-Original","Hazard-FalseAlarm","Probability")
AddTest -Testname "HeightControl_Original-Original-Original_Probability_Prevention-Collision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.Original-Original-Original" -TestNunitCategory "PreventionProbability" -TestCategories @("HeightControl","Variant-Original-Original-Original","Prevention-Collision","Probability")
AddTest -Testname "HeightControl_Original-Original-Original_DCCA_HazardCollision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.ModelCheckingTests.Original-Original-Original" -TestNunitCategory "CollisionDCCA" -TestCategories @("HeightControl","Variant-Original-Original-Original","Hazard-Collision","DCCA")
AddTest -Testname "HeightControl_Original-Original-Original_DCCA_HazardFalseAlarm" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.ModelCheckingTests.Original-Original-Original" -TestNunitCategory "FalseAlarmDCCA" -TestCategories @("HeightControl","Variant-Original-Original-Original","Hazard-FalseAlarm","DCCA")
AddTest -Testname "HeightControl_Original-Original-Original_EnumerateStateSpace" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.ModelCheckingTests.EnumerateAllStatesOriginalDesign" -TestNunitCategory "" -TestCategories @("HeightControl","Variant-Original-Original-Original","EnumerateStateSpace")

# OverheadDetectors-Tolerant-LightBarrier
AddTest -Testname "HeightControl_OverheadDetectors-Tolerant-LightBarrier_Probability_HazardCollision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.OverheadDetectors-Tolerant-LightBarrier" -TestNunitCategory "CollisionProbability" -TestCategories @("HeightControl","Variant-OverheadDetectors-Tolerant-LightBarrier","Hazard-Collision","Probability","Exceeds-Memory-Limit")
AddTest -Testname "HeightControl_OverheadDetectors-Tolerant-LightBarrier_Probability_HazardFalseAlarm" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.OverheadDetectors-Tolerant-LightBarrier" -TestNunitCategory "FalseAlarmProbability" -TestCategories @("HeightControl","Variant-OverheadDetectors-Tolerant-LightBarrier","Hazard-FalseAlarm","Probability","Exceeds-Memory-Limit")
AddTest -Testname "HeightControl_OverheadDetectors-Tolerant-LightBarrier_Probability_Prevention-Collision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.OverheadDetectors-Tolerant-LightBarrier" -TestNunitCategory "PreventionProbability" -TestCategories @("HeightControl","Variant-OverheadDetectors-Tolerant-LightBarrier","Prevention-Collision","Probability","Exceeds-Memory-Limit")
AddTest -Testname "HeightControl_OverheadDetectors-Tolerant-LightBarrier_DCCA_HazardCollision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.ModelCheckingTests.OverheadDetectors-Tolerant-LightBarrier" -TestNunitCategory "CollisionDCCA" -TestCategories @("HeightControl","Variant-OverheadDetectors-Tolerant-LightBarrier","Hazard-Collision","DCCA")
AddTest -Testname "HeightControl_OverheadDetectors-Tolerant-LightBarrier_DCCA_HazardFalseAlarm" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.ModelCheckingTests.OverheadDetectors-Tolerant-LightBarrier" -TestNunitCategory "FalseAlarmDCCA" -TestCategories @("HeightControl","Variant-OverheadDetectors-Tolerant-LightBarrier","Hazard-FalseAlarm","DCCA")

# OverheadDetectors-Tolerant-Original
AddTest -Testname "HeightControl_OverheadDetectors-Tolerant-Original_Probability_HazardCollision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.OverheadDetectors-Tolerant-Original" -TestNunitCategory "CollisionProbability" -TestCategories @("HeightControl","Variant-OverheadDetectors-Tolerant-Original","Hazard-Collision","Probability","Exceeds-Memory-Limit")
AddTest -Testname "HeightControl_OverheadDetectors-Tolerant-Original_Probability_HazardFalseAlarm" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.OverheadDetectors-Tolerant-Original" -TestNunitCategory "FalseAlarmProbability" -TestCategories @("HeightControl","Variant-OverheadDetectors-Tolerant-Original","Hazard-FalseAlarm","Probability","Exceeds-Memory-Limit")
AddTest -Testname "HeightControl_OverheadDetectors-Tolerant-Original_Probability_Prevention-Collision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.OverheadDetectors-Tolerant-Original" -TestNunitCategory "PreventionProbability" -TestCategories @("HeightControl","Variant-OverheadDetectors-Tolerant-Original","Prevention-Collision","Probability","Exceeds-Memory-Limit")
AddTest -Testname "HeightControl_OverheadDetectors-Tolerant-Original_DCCA_HazardCollision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.ModelCheckingTests.OverheadDetectors-Tolerant-Original" -TestNunitCategory "CollisionDCCA" -TestCategories @("HeightControl","Variant-OverheadDetectors-Tolerant-Original","Hazard-Collision","DCCA")
AddTest -Testname "HeightControl_OverheadDetectors-Tolerant-Original_DCCA_HazardFalseAlarm" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.ModelCheckingTests.OverheadDetectors-Tolerant-Original" -TestNunitCategory "FalseAlarmDCCA" -TestCategories @("HeightControl","Variant-OverheadDetectors-Tolerant-Original","Hazard-FalseAlarm","DCCA")

# OverheadDetectors-Original-LightBarrier
AddTest -Testname "HeightControl_OverheadDetectors-Original-LightBarrier_Probability_HazardCollision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.OverheadDetectors-Original-LightBarrier" -TestNunitCategory "CollisionProbability" -TestCategories @("HeightControl","Variant-OverheadDetectors-Original-LightBarrier","Hazard-Collision","Probability","Exceeds-Memory-Limit")
AddTest -Testname "HeightControl_OverheadDetectors-Original-LightBarrier_Probability_HazardFalseAlarm" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.OverheadDetectors-Original-LightBarrier" -TestNunitCategory "FalseAlarmProbability" -TestCategories @("HeightControl","Variant-OverheadDetectors-Original-LightBarrier","Hazard-FalseAlarm","Probability","Exceeds-Memory-Limit")
AddTest -Testname "HeightControl_OverheadDetectors-Original-LightBarrier_Probability_Prevention-Collision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.OverheadDetectors-Original-LightBarrier" -TestNunitCategory "PreventionProbability" -TestCategories @("HeightControl","Variant-OverheadDetectors-Original-LightBarrier","Prevention-Collision","Probability","Exceeds-Memory-Limit")
AddTest -Testname "HeightControl_OverheadDetectors-Original-LightBarrier_DCCA_HazardCollision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.ModelCheckingTests.OverheadDetectors-Original-LightBarrier" -TestNunitCategory "CollisionDCCA" -TestCategories @("HeightControl","Variant-OverheadDetectors-Original-LightBarrier","Hazard-Collision","DCCA")
AddTest -Testname "HeightControl_OverheadDetectors-Original-LightBarrier_DCCA_HazardFalseAlarm" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.ModelCheckingTests.OverheadDetectors-Original-LightBarrier" -TestNunitCategory "FalseAlarmDCCA" -TestCategories @("HeightControl","Variant-OverheadDetectors-Original-LightBarrier","Hazard-FalseAlarm","DCCA")

# OverheadDetectors-Original-Original
AddTest -Testname "HeightControl_OverheadDetectors-Original-Original_Probability_HazardCollision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.OverheadDetectors-Original-Original" -TestNunitCategory "CollisionProbability" -TestCategories @("HeightControl","Variant-OverheadDetectors-Original-Original","Hazard-Collision","Probability")
AddTest -Testname "HeightControl_OverheadDetectors-Original-Original_Probability_HazardFalseAlarm" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.OverheadDetectors-Original-Original" -TestNunitCategory "FalseAlarmProbability" -TestCategories @("HeightControl","Variant-OverheadDetectors-Original-Original","Hazard-FalseAlarm","Probability")
AddTest -Testname "HeightControl_OverheadDetectors-Original-Original_Probability_Prevention-Collision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.OverheadDetectors-Original-Original" -TestNunitCategory "PreventionProbability" -TestCategories @("HeightControl","Variant-OverheadDetectors-Original-Original","Prevention-Collision","Probability")
AddTest -Testname "HeightControl_OverheadDetectors-Original-Original_DCCA_HazardCollision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.ModelCheckingTests.OverheadDetectors-Original-Original" -TestNunitCategory "CollisionDCCA" -TestCategories @("HeightControl","Variant-OverheadDetectors-Original-Original","Hazard-Collision","DCCA")
AddTest -Testname "HeightControl_OverheadDetectors-Original-Original_DCCA_HazardFalseAlarm" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.ModelCheckingTests.OverheadDetectors-Original-Original" -TestNunitCategory "FalseAlarmDCCA" -TestCategories @("HeightControl","Variant-OverheadDetectors-Original-Original","Hazard-FalseAlarm","DCCA")

# Original-NoCounterTolerant-LightBarrier
AddTest -Testname "HeightControl_Original-NoCounterTolerant-LightBarrier_Probability_HazardCollision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.Original-NoCounterTolerant-LightBarrier" -TestNunitCategory "CollisionProbability" -TestCategories @("HeightControl","Variant-Original-NoCounterTolerant-LightBarrier","Hazard-Collision","Probability","Exceeds-Memory-Limit")
AddTest -Testname "HeightControl_Original-NoCounterTolerant-LightBarrier_Probability_HazardFalseAlarm" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.Original-NoCounterTolerant-LightBarrier" -TestNunitCategory "FalseAlarmProbability" -TestCategories @("HeightControl","Variant-Original-NoCounterTolerant-LightBarrier","Hazard-FalseAlarm","Probability","Exceeds-Memory-Limit")
AddTest -Testname "HeightControl_Original-NoCounterTolerant-LightBarrier_Probability_Prevention-Collision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.Original-NoCounterTolerant-LightBarrier" -TestNunitCategory "PreventionProbability" -TestCategories @("HeightControl","Variant-Original-NoCounterTolerant-LightBarrier","Prevention-Collision","Probability","Exceeds-Memory-Limit")
AddTest -Testname "HeightControl_Original-NoCounterTolerant-LightBarrier_DCCA_HazardCollision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.ModelCheckingTests.Original-NoCounterTolerant-LightBarrier" -TestNunitCategory "CollisionDCCA" -TestCategories @("HeightControl","Variant-Original-NoCounterTolerant-LightBarrier","Hazard-Collision","DCCA")
AddTest -Testname "HeightControl_Original-NoCounterTolerant-LightBarrier_DCCA_HazardFalseAlarm" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.ModelCheckingTests.Original-NoCounterTolerant-LightBarrier" -TestNunitCategory "FalseAlarmDCCA" -TestCategories @("HeightControl","Variant-Original-NoCounterTolerant-LightBarrier","Hazard-FalseAlarm","DCCA")

# Original-NoCounterTolerant-Original
AddTest -Testname "HeightControl_Original-NoCounterTolerant-Original_Probability_HazardCollision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.Original-NoCounterTolerant-Original" -TestNunitCategory "CollisionProbability" -TestCategories @("HeightControl","Variant-Original-NoCounterTolerant-Original","Hazard-Collision","Probability")
AddTest -Testname "HeightControl_Original-NoCounterTolerant-Original_Probability_HazardFalseAlarm" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.Original-NoCounterTolerant-Original" -TestNunitCategory "FalseAlarmProbability" -TestCategories @("HeightControl","Variant-Original-NoCounterTolerant-Original","Hazard-FalseAlarm","Probability")
AddTest -Testname "HeightControl_Original-NoCounterTolerant-Original_Probability_Prevention-Collision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.Original-NoCounterTolerant-Original" -TestNunitCategory "PreventionProbability" -TestCategories @("HeightControl","Variant-Original-NoCounterTolerant-Original","Prevention-Collision","Probability")
AddTest -Testname "HeightControl_Original-NoCounterTolerant-Original_DCCA_HazardCollision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.ModelCheckingTests.Original-NoCounterTolerant-Original" -TestNunitCategory "CollisionDCCA" -TestCategories @("HeightControl","Variant-Original-NoCounterTolerant-Original","Hazard-Collision","DCCA")
AddTest -Testname "HeightControl_Original-NoCounterTolerant-Original_DCCA_HazardFalseAlarm" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.ModelCheckingTests.Original-NoCounterTolerant-Original" -TestNunitCategory "FalseAlarmDCCA" -TestCategories @("HeightControl","Variant-Original-NoCounterTolerant-Original","Hazard-FalseAlarm","DCCA")

# Original-NoCounter-LightBarrier
AddTest -Testname "HeightControl_Original-NoCounter-LightBarrier_Probability_HazardCollision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.Original-NoCounter-LightBarrier" -TestNunitCategory "CollisionProbability" -TestCategories @("HeightControl","Variant-Original-NoCounter-LightBarrier","Hazard-Collision","Probability","Exceeds-Memory-Limit")
AddTest -Testname "HeightControl_Original-NoCounter-LightBarrier_Probability_HazardFalseAlarm" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.Original-NoCounter-LightBarrier" -TestNunitCategory "FalseAlarmProbability" -TestCategories @("HeightControl","Variant-Original-NoCounter-LightBarrier","Hazard-FalseAlarm","Probability","Exceeds-Memory-Limit")
AddTest -Testname "HeightControl_Original-NoCounter-LightBarrier_Probability_Prevention-Collision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.Original-NoCounter-LightBarrier" -TestNunitCategory "PreventionProbability" -TestCategories @("HeightControl","Variant-Original-NoCounter-LightBarrier","Prevention-Collision","Probability","Exceeds-Memory-Limit")
AddTest -Testname "HeightControl_Original-NoCounter-LightBarrier_DCCA_HazardCollision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.ModelCheckingTests.Original-NoCounter-LightBarrier" -TestNunitCategory "CollisionDCCA" -TestCategories @("HeightControl","Variant-Original-NoCounter-LightBarrier","Hazard-Collision","DCCA")
AddTest -Testname "HeightControl_Original-NoCounter-LightBarrier_DCCA_HazardFalseAlarm" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.ModelCheckingTests.Original-NoCounter-LightBarrier" -TestNunitCategory "FalseAlarmDCCA" -TestCategories @("HeightControl","Variant-Original-NoCounter-LightBarrier","Hazard-FalseAlarm","DCCA")

# Original-NoCounter-Original
AddTest -Testname "HeightControl_Original-NoCounter-Original_Probability_HazardCollision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.Original-NoCounter-Original" -TestNunitCategory "CollisionProbability" -TestCategories @("HeightControl","Variant-Original-NoCounter-Original","Hazard-Collision","Probability")
AddTest -Testname "HeightControl_Original-NoCounter-Original_Probability_HazardFalseAlarm" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.Original-NoCounter-Original" -TestNunitCategory "FalseAlarmProbability" -TestCategories @("HeightControl","Variant-Original-NoCounter-Original","Hazard-FalseAlarm","Probability")
AddTest -Testname "HeightControl_Original-NoCounter-Original_Probability_Prevention-Collision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.Original-NoCounter-Original" -TestNunitCategory "PreventionProbability" -TestCategories @("HeightControl","Variant-Original-NoCounter-Original","Prevention-Collision","Probability")
AddTest -Testname "HeightControl_Original-NoCounter-Original_DCCA_HazardCollision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.ModelCheckingTests.Original-NoCounter-Original" -TestNunitCategory "CollisionDCCA" -TestCategories @("HeightControl","Variant-Original-NoCounter-Original","Hazard-Collision","DCCA")
AddTest -Testname "HeightControl_Original-NoCounter-Original_DCCA_HazardFalseAlarm" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.ModelCheckingTests.Original-NoCounter-Original" -TestNunitCategory "FalseAlarmDCCA" -TestCategories @("HeightControl","Variant-Original-NoCounter-Original","Hazard-FalseAlarm","DCCA")

# Original-Tolerant-LightBarrier
AddTest -Testname "HeightControl_Original-Tolerant-LightBarrier_Probability_HazardCollision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.Original-Tolerant-LightBarrier" -TestNunitCategory "CollisionProbability" -TestCategories @("HeightControl","Variant-Original-Tolerant-LightBarrier","Hazard-Collision","Probability","Exceeds-Memory-Limit")
AddTest -Testname "HeightControl_Original-Tolerant-LightBarrier_Probability_HazardFalseAlarm" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.Original-Tolerant-LightBarrier" -TestNunitCategory "FalseAlarmProbability" -TestCategories @("HeightControl","Variant-Original-Tolerant-LightBarrier","Hazard-FalseAlarm","Probability","Exceeds-Memory-Limit")
AddTest -Testname "HeightControl_Original-Tolerant-LightBarrier_Probability_Prevention-Collision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.Original-Tolerant-LightBarrier" -TestNunitCategory "PreventionProbability" -TestCategories @("HeightControl","Variant-Original-Tolerant-LightBarrier","Prevention-Collision","Probability","Exceeds-Memory-Limit")
AddTest -Testname "HeightControl_Original-Tolerant-LightBarrier_DCCA_HazardCollision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.ModelCheckingTests.Original-Tolerant-LightBarrier" -TestNunitCategory "CollisionDCCA" -TestCategories @("HeightControl","Variant-Original-Tolerant-LightBarrier","Hazard-Collision","DCCA")
AddTest -Testname "HeightControl_Original-Tolerant-LightBarrier_DCCA_HazardFalseAlarm" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.ModelCheckingTests.Original-Tolerant-LightBarrier" -TestNunitCategory "FalseAlarmDCCA" -TestCategories @("HeightControl","Variant-Original-Tolerant-LightBarrier","Hazard-FalseAlarm","DCCA")

# Original-Tolerant-Original
AddTest -Testname "HeightControl_Original-Tolerant-Original_Probability_HazardCollision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.Original-Tolerant-Original" -TestNunitCategory "CollisionProbability" -TestCategories @("HeightControl","Variant-Original-Tolerant-Original","Hazard-Collision","Probability","Exceeds-Memory-Limit")
AddTest -Testname "HeightControl_Original-Tolerant-Original_Probability_HazardFalseAlarm" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.Original-Tolerant-Original" -TestNunitCategory "FalseAlarmProbability" -TestCategories @("HeightControl","Variant-Original-Tolerant-Original","Hazard-FalseAlarm","Probability","Exceeds-Memory-Limit")
AddTest -Testname "HeightControl_Original-Tolerant-Original_Probability_Prevention-Collision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.Original-Tolerant-Original" -TestNunitCategory "PreventionProbability" -TestCategories @("HeightControl","Variant-Original-Tolerant-Original","Prevention-Collision","Probability","Exceeds-Memory-Limit")
AddTest -Testname "HeightControl_Original-Tolerant-Original_DCCA_HazardCollision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.ModelCheckingTests.Original-Tolerant-Original" -TestNunitCategory "CollisionDCCA" -TestCategories @("HeightControl","Variant-Original-Tolerant-Original","Hazard-Collision","DCCA")
AddTest -Testname "HeightControl_Original-Tolerant-Original_DCCA_HazardFalseAlarm" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.ModelCheckingTests.Original-Tolerant-Original" -TestNunitCategory "FalseAlarmDCCA" -TestCategories @("HeightControl","Variant-Original-Tolerant-Original","Hazard-FalseAlarm","DCCA")

# Original-Original-LightBarrier
AddTest -Testname "HeightControl_Original-Original-LightBarrier_Probability_HazardCollision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.Original-Original-LightBarrier" -TestNunitCategory "CollisionProbability" -TestCategories @("HeightControl","Variant-Original-Original-LightBarrier","Hazard-Collision","Probability","Exceeds-Memory-Limit")
AddTest -Testname "HeightControl_Original-Original-LightBarrier_Probability_HazardFalseAlarm" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.Original-Original-LightBarrier" -TestNunitCategory "FalseAlarmProbability" -TestCategories @("HeightControl","Variant-Original-Original-LightBarrier","Hazard-FalseAlarm","Probability","Exceeds-Memory-Limit")
AddTest -Testname "HeightControl_Original-Original-LightBarrier_Probability_Prevention-Collision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.HazardProbabilityTests.Original-Original-LightBarrier" -TestNunitCategory "PreventionProbability" -TestCategories @("HeightControl","Variant-Original-Original-LightBarrier","Prevention-Collision","Probability","Exceeds-Memory-Limit")
AddTest -Testname "HeightControl_Original-Original-LightBarrier_DCCA_HazardCollision" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.ModelCheckingTests.Original-Original-LightBarrier" -TestNunitCategory "CollisionDCCA" -TestCategories @("HeightControl","Variant-Original-Original-LightBarrier","Hazard-Collision","DCCA")
AddTest -Testname "HeightControl_Original-Original-LightBarrier_DCCA_HazardFalseAlarm" -TestAssembly "SafetySharp.CaseStudies.HeightControl.dll" -TestMethod "SafetySharp.CaseStudies.HeightControl.Analysis.ModelCheckingTests.Original-Original-LightBarrier" -TestNunitCategory "FalseAlarmDCCA" -TestCategories @("HeightControl","Variant-Original-Original-LightBarrier","Hazard-FalseAlarm","DCCA")
